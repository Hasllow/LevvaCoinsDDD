using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync()
    {
        return Ok(await _categoryService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetByIdAsync(Guid id)
    {
        return Ok(await _categoryService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CategoryDTO category)
    {
        await _categoryService.CreateAsync(category);
        return Created("", category);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(CategoryUpdateDTO category)
    {
        await _categoryService.UpdateAsync(category);
        return NoContent();
    }
}
