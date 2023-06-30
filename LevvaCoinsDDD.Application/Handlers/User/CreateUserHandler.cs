using AutoMapper;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;
using UserModel = LevvaCoinsDDD.Domain.Models.User;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class CreateUserHandler : IRequestHandler<CreateUserRequest, ResponseApiDTO<CreateUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseApiDTO<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = _mapper.Map<UserModel>(request);
        userToCreate.Id = Guid.NewGuid();
        userToCreate.AvatarUrl = "URL DEFAULT";

        await _userRepository.CreateAsync(userToCreate);

        var response = _mapper.Map<CreateUserResponse>(userToCreate);

        return new ResponseApiDTO<CreateUserResponse>() { hasError = false, data = response };
    }
}