using LevvaCoinsDDD.API.Utilities;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Application.Validators.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(TransactionNewDTO transaction)
    {
        var validator = new TransactionNewDTOValidator();
        var validRes = validator.Validate(transaction);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.CreateAsync(transaction, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created(response.data.Id, response.data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.DeleteAsync(id, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponseByUserDTO>>> GetAllAsync()
    {
        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.GetAllAsync(token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.collectionData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionResponseByUserDTO>> GetByIdAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.GetByIdAsync(id, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, TransactionUpdateDTO transaction)
    {
        var validator = new TransactionUpdateDTOValidator();
        var validRes = validator.Validate(transaction);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.UpdateAsync(id, transaction, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }

    [HttpGet("search/{searchParam}")]
    public async Task<ActionResult<TransactionResponseByUserDTO>> SearchAsync(string searchParam)
    {
        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.SearchAsync(searchParam, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.collectionData);
    }
}
