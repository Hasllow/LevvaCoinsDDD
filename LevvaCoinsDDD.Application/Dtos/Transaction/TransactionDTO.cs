using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Dtos.User;

namespace LevvaCoinsDDD.Application.Dtos.Transaction;
public class TransactionDTO
{
    public string Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public int Type { get; set; }
    public DateTime Date { get; set; }

    public Guid UserID { get; set; }
    public Guid CategoryID { get; set; }

    public UserDTO User { get; set; }
    public CategoryDTO Category { get; set; }
}
