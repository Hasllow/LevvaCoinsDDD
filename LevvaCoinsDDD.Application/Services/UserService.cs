using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevvaCoinsDDD.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _config = config;
    }

    public async Task<ResponseApiDTO<UserDTO>> CreateAsync(UserNewAccountDTO newUser)
    {
        if (newUser.Password != newUser.ConfirmPassword) return new ResponseApiDTO<UserDTO> { hasError = true, message = "Senhas não coincidem." };

        var emailIsValid = new EmailAddressAttribute().IsValid(newUser.Email);
        if (!emailIsValid) return new ResponseApiDTO<UserDTO> { hasError = true, message = "E-mail inválido." };

        var emailAlreadyUsed = await _userRepository.GetByEmailAsync(newUser.Email);
        if (emailAlreadyUsed != null) return new ResponseApiDTO<UserDTO> { hasError = true, message = "E-mail já cadastrado." };

        var userToCreate = _mapper.Map<User>(newUser);
        userToCreate.Id = Guid.NewGuid();
        userToCreate.AvatarUrl = "URL DEFAULT";
        await _userRepository.CreateAsync(userToCreate);

        return new ResponseApiDTO<UserDTO> { hasError = false, data = _mapper.Map<UserDTO>(userToCreate) };
    }

    public async Task<ResponseApiDTO<bool>> DeleteAsync(string id)
    {
        var guidId = Guid.Parse(id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<bool> { hasError = true, message = "Esse usuário não existe." };

        await _userRepository.DeleteAsync(guidId);

        return new ResponseApiDTO<bool> { hasError = false };
    }

    public async Task<ResponseApiDTO<UserDTO>> GetAllAsync()
    {
        var response = await _userRepository.GetAllAsync();
        var mappedUsers = _mapper.Map<IEnumerable<UserDTO>>(response);

        return new ResponseApiDTO<UserDTO> { hasError = false, collectionData = mappedUsers };
    }

    public async Task<ResponseApiDTO<UserDTO>> GetByIdAsync(string id)
    {
        var guidId = Guid.Parse(id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<UserDTO> { hasError = true, message = "Esse usuário não existe." };

        var mappedUser = _mapper.Map<UserDTO>(user);

        return new ResponseApiDTO<UserDTO> { hasError = false, data = mappedUser };
    }

    public async Task<ResponseApiDTO<LoginValuesDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _userRepository.GetByEmailAndPasswordAsync(loginDTO.Email, loginDTO.Password);

        if (user == null) return new ResponseApiDTO<LoginValuesDTO> { hasError = true, message = "Usuário ou senha inválidos." };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetSection("Secret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
           {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
           }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var loginValues = _mapper.Map<LoginValuesDTO>(user);

        loginValues.Token = "Bearer " + tokenHandler.WriteToken(token);

        return new ResponseApiDTO<LoginValuesDTO> { hasError = false, data = loginValues };
    }

    public async Task<ResponseApiDTO<bool>> UpdateAsync(string id, UserUpdateDTO entity)
    {
        var guidId = Guid.Parse(id);

        var user = await _userRepository.GetByIdAsync(guidId);

        if (user == null) return new ResponseApiDTO<bool> { hasError = true, message = "Esse usuário não existe." };

        if (!entity.Email.IsNullOrEmpty())
        {
            var emailIsValid = new EmailAddressAttribute().IsValid(entity.Email);
            if (!emailIsValid) return new ResponseApiDTO<bool> { hasError = true, message = "E-mail inválido." };

            var emailAlreadyUsed = await _userRepository.GetByEmailAsync(entity.Email);
            if (emailAlreadyUsed != null && emailAlreadyUsed.Id != user.Id) return new ResponseApiDTO<bool> { hasError = true, message = "E-mail já cadastrado." };
            user.Email = entity.Email;
        }

        user.Name = entity.Name.IsNullOrEmpty() ? user.Name : entity.Name; ;

        await _userRepository.UpdateAsync(user);

        return new ResponseApiDTO<bool> { hasError = false };
    }
}
