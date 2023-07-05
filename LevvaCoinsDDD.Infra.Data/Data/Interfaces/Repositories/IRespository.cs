namespace LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;

public interface IRespository<TEntity> where TEntity : class
{
    public Task CreateAsync(TEntity entity);
    public Task DeleteAsync(Guid id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<TEntity> GetByIdAsync(Guid id);
    public Task UpdateAsync(TEntity entity);
}
