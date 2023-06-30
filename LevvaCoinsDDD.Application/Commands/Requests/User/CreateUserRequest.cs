using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos;
using MediatR;

namespace LevvaCoinsDDD.Application.Commands.Requests.User;
public class CreateUserRequest : IRequest<ResponseApiDTO<CreateUserResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
