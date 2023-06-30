using LevvaCoinsDDD.Application.Dtos;
using MediatR;

namespace LevvaCoinsDDD.Application.Commands.Requests.User;
public class DeleteUserRequest : IRequest<ResponseApiDTO<bool>>
{
    public DeleteUserRequest(string id) => Id = id;

    public string Id { get; set; }
}
