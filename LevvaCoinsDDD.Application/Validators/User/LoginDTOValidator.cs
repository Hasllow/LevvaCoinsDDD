using FluentValidation;
using LevvaCoinsDDD.Application.Dtos.User;

namespace LevvaCoinsDDD.Application.Validators.User;
public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail não pode ser nulo.")
            .EmailAddress().WithMessage("E-mail com formato invalido.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Senha não pode ser nula.");
    }
}
