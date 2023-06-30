using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Queries.Requests.User;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, ResponseApiDTO<GetAllUsersResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseApiDTO<GetAllUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var response = await _userRepository.GetAllAsync();
        var mappedUsers = _mapper.Map<IEnumerable<GetAllUsersResponse>>(response);

        return new ResponseApiDTO<GetAllUsersResponse> { hasError = false, collectionData = mappedUsers };
    }
}
