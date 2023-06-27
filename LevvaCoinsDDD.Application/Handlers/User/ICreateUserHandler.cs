using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;

namespace LevvaCoinsDDD.Application.Handlers.User;
public interface ICreateUserHandler
{
    Task<CreateUserResponse> Handle(CreateUserRequest command);
}
