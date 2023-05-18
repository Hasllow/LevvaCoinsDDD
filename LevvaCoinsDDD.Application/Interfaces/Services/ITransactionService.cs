using LevvaCoinsDDD.Application.Dtos;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface ITransactionService : IService<TransactionDTO, TransactionUpdateDTO>
{
}
