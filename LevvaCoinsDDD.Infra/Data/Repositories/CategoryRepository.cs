using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Context;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LevvaCoinsDDD.Infra.Data.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDBContext _context;

    public CategoryRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Category entity)
    {
        _context.Category.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var userToDelete = await GetByIdAsync(id);
        _context.Category.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Category.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _context.Category.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Category.Update(entity);
        await _context.SaveChangesAsync();
    }
}
