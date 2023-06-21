﻿using FluentValidation;
using LevvaCoinsDDD.Application.Dtos.Transaction;

namespace LevvaCoinsDDD.Application.Validators.Transactions;
public class TransactionUpdateDTOValidator : AbstractValidator<TransactionUpdateDTO>
{
    public TransactionUpdateDTOValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.UserID).NotEmpty();
        RuleFor(x => x.CategoryID).NotEmpty();
    }
}
