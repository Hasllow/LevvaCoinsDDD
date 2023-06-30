using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.User;
using MediatR;

namespace LevvaCoinsDDD.Application.Commands.Requests.User;
public class UpdateUserRequest : IRequest<ResponseApiDTO<bool>>
{
    public UpdateUserRequest(string id, UserUpdateDTO user)
    {
        Id = id;
        User = user;
    }

    public string Id { get; set; }
    public UserUpdateDTO User { get; set; }
}
