﻿using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
public interface ITransactionRepository : IRespository<Transaction>
{
    public Task<Transaction> CreateAndReturnAsync(Transaction entity);
    public Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
    public IQueryable<Transaction> Search(string searchParam, Guid userID);
}
