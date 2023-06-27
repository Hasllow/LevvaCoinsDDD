using LevvaCoinsDDD.Application.Commands.Response.User;
using MediatR;

namespace LevvaCoinsDDD.Application.Commands.Requests.User;
public class CreateUserRequest : IRequest<CreateUserResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
