using LevvaCoinsDDD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LevvaCoinsDDD.Infra.Data.Context;
public class AppDBContext : DbContext
{

    public AppDBContext()
    {

    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Category> Category { get; set; }
}
