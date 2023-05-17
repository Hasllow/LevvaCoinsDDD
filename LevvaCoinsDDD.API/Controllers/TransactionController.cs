using LevvaCoinsDDD.Application.Dtos;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllAsync()
    {
        return Ok(await _transactionService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDTO>> GetByIdAsync(Guid id)
    {
        return Ok(await _transactionService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(TransactionDTO transaction)
    {
        await _transactionService.CreateAsync(transaction);
        return Created("", transaction);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _transactionService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(TransactionUpdateDTO transaction)
    {
        await _transactionService.UpdateAsync(transaction);
        return NoContent();
    }
}
