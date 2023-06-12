using LevvaCoinsDDD.Domain.Enums;

namespace LevvaCoinsDDD.Application.Dtos.Transaction;
public class TransactionNewDTO
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ETransaction Type { get; set; }
    public string CategoryID { get; set; } = string.Empty;
}
