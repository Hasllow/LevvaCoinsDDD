using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Context;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LevvaCoinsDDD.Infra.Data.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDBContext _context;

    public UserRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User entity)
    {
        _context.User.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var userToDelete = await GetByIdAsync(id);
        _context.User.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task UpdateAsync(User entity)
    {
        _context.User.Update(entity);
        await _context.SaveChangesAsync();
    }
}
