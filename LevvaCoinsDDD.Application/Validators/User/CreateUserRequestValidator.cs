using FluentValidation;
using LevvaCoinsDDD.Application.Commands.Requests.User;

namespace LevvaCoinsDDD.Application.Validators.User;
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail não pode ser nulo.")
            .EmailAddress().WithMessage("E-mail com formato invalido.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Senha não pode ser nula.");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirmção de senha não pode ser nula.");
    }
}
