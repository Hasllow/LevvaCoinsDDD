using LevvaCoinsDDD.API.Utilities;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Application.Validators.Category;
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

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CategoryNewAndUpdateDTO category)
    {
        var validator = new CategoryNewAndUpdateValidator();
        var validRes = validator.Validate(category);

        if (!validRes.IsValid) return BadRequest(new { hasError = true, message = validRes.Errors.FirstOrDefault()?.ErrorMessage });

        var response = await _categoryService.CreateAsync(category);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Created("response.data.Id", response.data);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var response = await _categoryService.DeleteAsync(id);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync()
    {
        var response = await _categoryService.GetAllAsync();

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.collectionData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetByIdAsync(string id)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var response = await _categoryService.GetByIdAsync(id);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return Ok(response.data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, CategoryNewAndUpdateDTO category)
    {
        if (!IdValidator.IsValidIdFormat(id)) return BadRequest(new { hasError = true, message = "Id Inválida." });

        var response = await _categoryService.UpdateAsync(id, category);

        if (response.hasError) return BadRequest(new { response.hasError, response.message });

        return NoContent();
    }
}
