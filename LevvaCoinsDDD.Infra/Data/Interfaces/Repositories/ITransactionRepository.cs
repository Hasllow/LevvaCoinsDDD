using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
public interface ITransactionRepository : IRespository<Transaction>
{
    public new Task<Transaction> CreateAndReturnAsync(Transaction entity);
    public Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
}
