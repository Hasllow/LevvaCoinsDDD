using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevvaCoinsDDD.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRespository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public UserService(IUserRepository userRespository, IMapper mapper, IConfiguration config)
    {
        _userRespository = userRespository;
        _mapper = mapper;
        _config = config;
    }

    public async Task CreateAsync(UserDTO entity)
    {
        var userToCreate = _mapper.Map<User>(entity);
        userToCreate.Id = Guid.NewGuid();
        await _userRespository.CreateAsync(userToCreate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRespository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<UserDTO>>(await _userRespository.GetAllAsync());
    }

    public async Task<UserDTO> GetByIdAsync(Guid id)
    {
        return _mapper.Map<UserDTO>(await _userRespository.GetByIdAsync(id));
    }

    public async Task<LoginDTO> Login(LoginDTO loginDTO)
    {
        var user = await _userRespository.GetByEmailAndPasswordAsync(loginDTO.Email, loginDTO.Password);

        if (user == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetSection("Secret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
           {
                    new Claim(ClaimTypes.Name, user.Email)
           }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        loginDTO.Name = user.Name;
        loginDTO.Email = user.Email;
        loginDTO.Password = null;
        loginDTO.Token = tokenHandler.WriteToken(token);

        return loginDTO;
    }

    public async Task UpdateAsync(UserUpdateDTO entity)
    {
        await _userRespository.UpdateAsync(_mapper.Map<User>(entity));
    }
}
