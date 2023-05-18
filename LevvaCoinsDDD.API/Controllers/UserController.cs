using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateAsync(UserDTO user)
    {
        await _userService.CreateAsync(user);
        return Created("", user);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UserUpdateDTO user)
    {
        await _userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginDTO>> Login(LoginDTO loginDto)
    {
        var login = await _userService.Login(loginDto);

        if (login == null)
            return BadRequest("Usuário ou senha inválidos");

        return Ok(login);
    }
}
