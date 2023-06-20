using FluentValidation;
using LevvaCoinsDDD.Application.Dtos.Category;

namespace LevvaCoinsDDD.Application.Validators;
public class CategoryNewAndUpdateValidator : AbstractValidator<CategoryNewAndUpdateDTO>
{
    public CategoryNewAndUpdateValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrição não pode ser nula.");
    }
}
