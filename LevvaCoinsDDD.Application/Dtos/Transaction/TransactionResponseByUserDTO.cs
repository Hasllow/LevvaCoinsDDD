using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Domain.Enums;

namespace LevvaCoinsDDD.Application.Dtos.Transaction;
public class TransactionResponseByUserDTO
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ETransaction Type { get; set; }
    public DateTime Date { get; set; }

    public CategoryDTO? Category { get; set; }
}
