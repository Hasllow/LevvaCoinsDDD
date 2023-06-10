using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
public interface IUserRepository : IRespository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByEmailAndPasswordAsync(string email, string password);
}
