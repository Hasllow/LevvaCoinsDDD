using FluentValidation;
using LevvaCoinsDDD.Application.Dtos.Transaction;

namespace LevvaCoinsDDD.Application.Validators.Transactions;
public class TransactionNewDTOValidator : AbstractValidator<TransactionNewDTO>
{
    public TransactionNewDTOValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.CategoryID).NotEmpty();
    }
}
