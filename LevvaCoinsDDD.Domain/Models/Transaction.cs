using LevvaCoinsDDD.Domain.Enums;

namespace LevvaCoinsDDD.Domain.Models;
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
