using AutoMapper;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevvaCoinsDDD.Application.Handlers.User;
public class LoginUserHandler : IRequestHandler<LoginUserRequest, ResponseApiDTO<LoginUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public LoginUserHandler(IUserRepository userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _config = config;
    }

    public async Task<ResponseApiDTO<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);

        if (user == null) return new ResponseApiDTO<LoginUserResponse> { hasError = true, message = "Usuário ou senha inválidos." };

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

        var loginValues = _mapper.Map<LoginUserResponse>(user);

        loginValues.Token = "Bearer " + tokenHandler.WriteToken(token);

        return new ResponseApiDTO<LoginUserResponse> { hasError = false, data = loginValues };
    }
}
