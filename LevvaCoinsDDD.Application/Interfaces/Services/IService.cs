using LevvaCoinsDDD.Application.Dtos;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface IService<TNewEntity, TEntity, TUpdateEntity, TCreateResponse, TDeleteResponse, TGetResponse, TUpdateResponse>
{
    public Task<ResponseApiDTO<TCreateResponse>> CreateAsync(TNewEntity newEntity);
    public Task<ResponseApiDTO<TDeleteResponse>> DeleteAsync(string id);
    public Task<ResponseApiDTO<TGetResponse>> GetAllAsync();
    public Task<ResponseApiDTO<TGetResponse>> GetByIdAsync(string id);
    public Task<ResponseApiDTO<TUpdateResponse>> UpdateAsync(string id, TUpdateEntity entity);
}
