using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using MediatR;

namespace LevvaCoinsDDD.Application.Queries.Requests.User;
public class GetAllUsersRequest : IRequest<ResponseApiDTO<GetAllUsersResponse>>
{
}
