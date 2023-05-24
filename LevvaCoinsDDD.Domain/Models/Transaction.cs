using LevvaCoinsDDD.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace LevvaCoinsDDD.Domain.Models;
[ExcludeFromCodeCoverage]
public class Transaction : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public ETransaction Type { get; set; }
    public DateTime Date { get; set; }

    public Guid UserID { get; set; }
    public virtual User User { get; set; }

    public Guid CategoryID { get; set; }
    public virtual Category Category { get; set; }

}
