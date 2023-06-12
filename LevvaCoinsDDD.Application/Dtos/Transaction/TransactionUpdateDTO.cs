using LevvaCoinsDDD.Domain.Enums;

namespace LevvaCoinsDDD.Application.Dtos.Transaction;
public class TransactionUpdateDTO
{
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public ETransaction? Type { get; set; }
    public DateTime? Date { get; set; }

    public string? UserID { get; set; }
    public string? CategoryID { get; set; }
}
