using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Interfaces.Services;
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
        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.CreateAsync(transaction, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created(response.data.Id, response.data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
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
        Request.Headers.TryGetValue("Authorization", out var token);
        var response = await _transactionService.GetByIdAsync(id, token);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, TransactionUpdateDTO transaction)
    {
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
