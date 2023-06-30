using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using MediatR;

namespace LevvaCoinsDDD.Application.Queries.Requests.User;
public class GetUserByIdRequest : IRequest<ResponseApiDTO<GetUserByIdResponse>>
{
    public string Id { get; set; } = string.Empty;
}
