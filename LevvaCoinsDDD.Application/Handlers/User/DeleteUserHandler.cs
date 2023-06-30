using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, ResponseApiDTO<bool>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseApiDTO<bool>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var guidId = Guid.Parse(request.Id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<bool> { hasError = true, message = "Esse usuário não existe." };

        await _userRepository.DeleteAsync(guidId);

        return new ResponseApiDTO<bool> { hasError = false };
    }
}