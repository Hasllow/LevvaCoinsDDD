using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos;
using MediatR;

namespace LevvaCoinsDDD.Application.Commands.Requests.User;
public class LoginUserRequest : IRequest<ResponseApiDTO<LoginUserResponse>>
{
    public LoginUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
