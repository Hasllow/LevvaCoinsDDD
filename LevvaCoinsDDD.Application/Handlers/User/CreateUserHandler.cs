using AutoMapper;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using UserModel = LevvaCoinsDDD.Domain.Models.User;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class CreateUserHandler : ICreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest command)
    {
        var userToCreate = _mapper.Map<UserModel>(command);
        userToCreate.Id = Guid.NewGuid();
        userToCreate.AvatarUrl = "URL DEFAULT";

        await _userRepository.CreateAsync(userToCreate);

        var response = _mapper.Map<CreateUserResponse>(userToCreate);

        return response;
    }
}
