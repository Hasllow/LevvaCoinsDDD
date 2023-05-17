namespace LevvaCoinsDDD.Application.Dtos;
public class TransactionDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }

    public Guid UserID { get; set; }
    public Guid CategoryID { get; set; }
}
