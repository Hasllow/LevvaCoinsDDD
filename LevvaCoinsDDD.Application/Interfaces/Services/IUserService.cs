using LevvaCoinsDDD.Application.Dtos;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface IUserService : IService<UserDTO, UserUpdateDTO>
{
    Task<LoginDTO> Login(LoginDTO loginDTO);
}
