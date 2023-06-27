using LevvaCoinsDDD.API.Utilities;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Application.Handlers.User;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Application.Validators.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICreateUserHandler _createUserHandler;

    public UserController(IUserService userService, ICreateUserHandler createUserHandler)
    {
        _userService = userService;
        _createUserHandler = createUserHandler;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> CreateAsync(UserNewAccountDTO newUser)
    {
        var validator = new UserNewAccountDTOValidator();
        var validRes = validator.Validate(newUser);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _userService.CreateAsync(newUser);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created(response.data.Id, response.data);
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> CreateWithRequestAsync(CreateUserRequest command)
    {
        //var validator = new UserNewAccountDTOValidator();
        //var validRes = validator.Validate(newUser);

        //if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _createUserHandler.Handle(command);

        //if (response.hasError) return BadRequest(new { response.hasError, response.message });

        //return Created(response.data.Id, response.data);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

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
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var response = await _userService.GetByIdAsync(id);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginValuesDTO>> Login(LoginDTO loginDTO)
    {
        var validator = new LoginDTOValidator();
        var validRes = validator.Validate(loginDTO);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _userService.Login(loginDTO);

        if (response.hasError)
            return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, UserUpdateDTO user)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var response = await _userService.UpdateAsync(id, user);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }
}
