namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface IService<TEntity, TUpdateEntity> where TEntity : class
{
    public Task CreateAsync(TEntity entity);
    public Task DeleteAsync(Guid id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<TEntity> GetByIdAsync(Guid id);
    public Task UpdateAsync(TUpdateEntity entity);
}
