using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.Transaction;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface ITransactionService
{
    public Task<ResponseApiDTO<TransactionResponseByUserDTO>> CreateAsync(TransactionNewDTO entity, string token);
    public Task<ResponseApiDTO<bool>> DeleteAsync(string transactionId, string token);
    public Task<ResponseApiDTO<TransactionResponseByUserDTO>> GetAllAsync(string token);
    public Task<ResponseApiDTO<TransactionResponseByUserDTO>> GetByIdAsync(string transactionId, string token);
    public Task<ResponseApiDTO<TransactionResponseByUserDTO>> SearchAsync(string searchParam, string token);
    public Task<ResponseApiDTO<bool>> UpdateAsync(string transactionId, TransactionUpdateDTO entity, string token);
}
