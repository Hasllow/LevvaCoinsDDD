using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Context;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LevvaCoinsDDD.Infra.Data.Repositories;
public class TransactionRepository : ITransactionRepository
{
    private readonly AppDBContext _context;

    public TransactionRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Transaction entity)
    {
        _context.Transaction.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var transactionToDelete = await GetByIdAsync(id);
        _context.Transaction.Remove(transactionToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transaction.ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        return await _context.Transaction.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task UpdateAsync(Transaction entity)
    {
        _context.Transaction.Update(entity);
        await _context.SaveChangesAsync();
    }
}
