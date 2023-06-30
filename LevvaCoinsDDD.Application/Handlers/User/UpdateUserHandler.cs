using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, ResponseApiDTO<bool>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseApiDTO<bool>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var guidId = Guid.Parse(request.Id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<bool> { hasError = true, message = "Esse usuário não existe." };

        if (!request.User.Email.IsNullOrEmpty())
        {
            var emailIsValid = new EmailAddressAttribute().IsValid(request.User.Email);
            if (!emailIsValid) return new ResponseApiDTO<bool> { hasError = true, message = "E-mail inválido." };

            var emailAlreadyUsed = await _userRepository.GetByEmailAsync(request.User.Email);
            if (emailAlreadyUsed != null && emailAlreadyUsed.Id != user.Id) return new ResponseApiDTO<bool> { hasError = true, message = "E-mail já cadastrado." };
            user.Email = request.User.Email;
        }

        user.Name = request.User.Name.IsNullOrEmpty() ? user.Name : request.User.Name; ;

        await _userRepository.UpdateAsync(user);

        return new ResponseApiDTO<bool> { hasError = false };
    }
}
