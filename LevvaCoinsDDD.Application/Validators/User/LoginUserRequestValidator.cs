using FluentValidation;
using LevvaCoinsDDD.Application.Commands.Requests.User;

namespace LevvaCoinsDDD.Application.Validators.User;
public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail não pode ser nulo.")
            .EmailAddress().WithMessage("E-mail com formato invalido.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Senha não pode ser nula.");
    }
}
