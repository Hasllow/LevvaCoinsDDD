using FluentValidation;
using LevvaCoinsDDD.Application.Dtos.Category;

namespace LevvaCoinsDDD.Application.Validators.Category;
public class CategoryValidator : AbstractValidator<CategoryDTO>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id não pode ser nula.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrição não pode ser nula.");
    }
}
