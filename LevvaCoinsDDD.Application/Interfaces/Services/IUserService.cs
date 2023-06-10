using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.User;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface IUserService : IService<UserNewAccountDTO, UserDTO, UserUpdateDTO, UserDTO, bool, UserDTO, bool>
{
    Task<ResponseApiDTO<LoginValuesDTO>> Login(LoginDTO loginDTO);
}
