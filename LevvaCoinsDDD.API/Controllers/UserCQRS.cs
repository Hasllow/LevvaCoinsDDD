using LevvaCoinsDDD.API.Utilities;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Application.Queries.Requests.User;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using LevvaCoinsDDD.Application.Validators.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserCQRS : ControllerBase
{
    private readonly IMediator _mediator;

    public UserCQRS(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(CreateUserRequest newUser)
    {
        var validator = new CreateUserRequestValidator();
        var validRes = validator.Validate(newUser);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _mediator.Send(newUser);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created(response.data.Id, response.data);
    }

    [HttpDelete]
    [AllowAnonymous]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var request = new DeleteUserRequest(id);

        var response = await _mediator.Send(request);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllUsersRequest());

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.collectionData);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<GetUserByIdResponse>> GetAsync(string id)
    {
        var request = new GetUserByIdRequest() { Id = id };

        var response = await _mediator.Send(request);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginValuesDTO>> Login(LoginUserRequest login)
    {
        var validator = new LoginUserRequestValidator();
        var validRes = validator.Validate(login);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _mediator.Send(login);

        if (response.hasError)
            return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> UpdateAsync(string id, UserUpdateDTO user)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var request = new UpdateUserRequest(id, user);

        var response = await _mediator.Send(request);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }
}
