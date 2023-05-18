using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;

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

    public async Task CreateAsync(TransactionDTO entity)
    {
        var transactionToCreate = _mapper.Map<Transaction>(entity);
        transactionToCreate.Id = Guid.NewGuid();
        await _transactionRepository.CreateAsync(transactionToCreate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _transactionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<TransactionDTO>>(await _transactionRepository.GetAllAsync());
    }

    public async Task<TransactionDTO> GetByIdAsync(Guid id)
    {
        return _mapper.Map<TransactionDTO>(await _transactionRepository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(TransactionUpdateDTO entity)
    {
        await _transactionRepository.UpdateAsync(_mapper.Map<Transaction>(entity));
    }
}
