using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Queries.Requests.User;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, ResponseApiDTO<GetUserByIdResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseApiDTO<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var guidId = Guid.Parse(request.Id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<GetUserByIdResponse> { hasError = true, message = "Esse usuário não existe." };

        var mappedUser = _mapper.Map<GetUserByIdResponse>(user);
        return new ResponseApiDTO<GetUserByIdResponse> { hasError = true, data = mappedUser };
    }
}
