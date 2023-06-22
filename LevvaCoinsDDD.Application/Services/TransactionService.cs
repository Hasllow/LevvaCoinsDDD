using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace LevvaCoinsDDD.Application.Services;
public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<ResponseApiDTO<TransactionResponseByUserDTO>> CreateAsync(TransactionNewDTO entity, string token)
    {
        var cleanToken = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(cleanToken);
        var idFromToken = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;

        var transactionToCreate = _mapper.Map<Transaction>(entity);
        transactionToCreate.Id = Guid.NewGuid();
        transactionToCreate.UserID = Guid.Parse(idFromToken);
        transactionToCreate.Date = DateTime.Now;

        await _transactionRepository.CreateAndReturnAsync(transactionToCreate);
        return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = false, data = _mapper.Map<TransactionResponseByUserDTO>(transactionToCreate) };
    }

    public async Task<ResponseApiDTO<bool>> DeleteAsync(string transactionId, string token)
    {
        var guidId = Guid.Parse(transactionId);

        var tokenDecoded = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokenDecoded);
        var idFromToken = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;

        var transaction = await _transactionRepository.GetByIdAsync(guidId);

        if (transaction == null) return new ResponseApiDTO<bool> { hasError = true, message = "Essa transação não existe." };
        if (idFromToken != transaction.UserID.ToString()) return new ResponseApiDTO<bool> { hasError = true, message = "Usuário não autorizado." };

        await _transactionRepository.DeleteAsync(guidId);

        return new ResponseApiDTO<bool> { hasError = false };
    }

    public async Task<ResponseApiDTO<TransactionResponseByUserDTO>> GetAllAsync(string token)
    {
        var tokenDecoded = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokenDecoded);
        var idFromToken = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;

        var hasConvertedId = Guid.TryParse(idFromToken, out var userGuidId);

        if (!hasConvertedId) return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = true, message = "ID inválida." };

        var transactions = await _transactionRepository.GetByUserIdAsync(userGuidId);
        var mappedTransactions = _mapper.Map<IEnumerable<TransactionResponseByUserDTO>>(transactions);

        return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = false, collectionData = mappedTransactions };
    }

    public async Task<ResponseApiDTO<TransactionResponseByUserDTO>> GetByIdAsync(string transactionId, string token)
    {
        var guidId = Guid.Parse(transactionId);

        var tokenDecoded = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokenDecoded);
        var idFromToken = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;

        var transaction = await _transactionRepository.GetByIdAsync(guidId);

        if (idFromToken != transaction.UserID.ToString()) return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = true, message = "Usuário não autorizado." };
        if (transaction == null) return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = true, message = "Essa transação não existe." };

        var mappedTransaction = _mapper.Map<TransactionResponseByUserDTO>(transaction);

        return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = false, data = mappedTransaction };
    }

    public async Task<ResponseApiDTO<TransactionResponseByUserDTO>> SearchAsync(string searchParam, string token)
    {
        var allTransactions = await GetAllAsync(token);

        var filteredTransactions = allTransactions.collectionData.Where(transaction =>
            transaction.Description.Contains(searchParam, StringComparison.InvariantCultureIgnoreCase) ||
            transaction.Amount.ToString().Contains(searchParam, StringComparison.InvariantCultureIgnoreCase) ||
            Enum.GetName(transaction.Type).Contains(searchParam, StringComparison.InvariantCultureIgnoreCase) ||
            transaction.Category.Description.Contains(searchParam, StringComparison.InvariantCultureIgnoreCase)).ToList();

        return new ResponseApiDTO<TransactionResponseByUserDTO> { hasError = false, collectionData = filteredTransactions };
    }

    public async Task<ResponseApiDTO<bool>> UpdateAsync(string transactionId, TransactionUpdateDTO entity, string token)
    {
        var guidId = Guid.Parse(transactionId);

        var tokenDecoded = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(tokenDecoded);
        var idFromToken = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;

        var transaction = await _transactionRepository.GetByIdAsync(guidId);

        if (idFromToken != transaction.UserID.ToString()) return new ResponseApiDTO<bool> { hasError = true, message = "Usuário não autorizado." };
        if (transaction == null) return new ResponseApiDTO<bool> { hasError = true, message = "Essa transação não existe." };

        transaction.Description = entity.Description ?? transaction.Description;
        transaction.Amount = entity.Amount ?? transaction.Amount;
        transaction.Type = entity.Type ?? transaction.Type;
        transaction.Date = entity.Date ?? transaction.Date;
        transaction.UserID = entity.UserID != null ? Guid.Parse(entity.UserID) : transaction.UserID;
        transaction.CategoryID = entity.CategoryID != null ? Guid.Parse(entity.CategoryID) : transaction.CategoryID;

        try
        {
            await _transactionRepository.UpdateAsync(transaction);
            return new ResponseApiDTO<bool> { hasError = false };
        }
        catch (Exception ex)
        {
            return new ResponseApiDTO<bool> { hasError = true, message = "Algo de errado aconteceu." };
        }
    }
}
