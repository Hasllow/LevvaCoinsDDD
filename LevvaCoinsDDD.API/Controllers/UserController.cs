using LevvaCoinsDDD.Application.Dtos.User;
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

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> CreateAsync(UserNewAccountDTO newUser)
    {
        var response = await _userService.CreateAsync(newUser);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created(response.data.Id, response.data);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        var response = await _userService.DeleteAsync(id);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync()
    {
        var response = await _userService.GetAllAsync();

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.collectionData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(string id)
    {
        var response = await _userService.GetByIdAsync(id);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginValuesDTO>> Login(LoginDTO loginDTO)
    {
        var response = await _userService.Login(loginDTO);

        if (response.hasError)
            return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, UserUpdateDTO user)
    {
        var response = await _userService.UpdateAsync(id, user);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }
}
