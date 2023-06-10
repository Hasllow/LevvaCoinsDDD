using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Interfaces.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;

namespace LevvaCoinsDDD.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ResponseApiDTO<CategoryDTO>> CreateAsync(CategoryNewAndUpdateDTO newEntity)
    {
        var categoryToCreate = _mapper.Map<Category>(newEntity);
        categoryToCreate.Id = Guid.NewGuid();

        var allCategories = await _categoryRepository.GetAllAsync();
        var nameAlreadyUsed = allCategories.Where(x => x.Description == categoryToCreate.Description).ToList();

        if (nameAlreadyUsed.Count > 0) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "Uma categoria com esse nome já existe." };

        await _categoryRepository.CreateAsync(categoryToCreate);

        return new ResponseApiDTO<CategoryDTO> { hasError = false, data = _mapper.Map<CategoryDTO>(categoryToCreate) };
    }

    public async Task<ResponseApiDTO<bool>> DeleteAsync(string id)
    {
        var hasConvertedId = Guid.TryParse(id, out var guidId);

        if (!hasConvertedId) return new ResponseApiDTO<bool> { hasError = true, message = "ID inválido." };

        var category = await _categoryRepository.GetByIdAsync(guidId);
        if (category == null) return new ResponseApiDTO<bool> { hasError = true, message = "Essa categoria não existe." };

        await _categoryRepository.DeleteAsync(guidId);

        return new ResponseApiDTO<bool> { hasError = false };
    }

    public async Task<ResponseApiDTO<CategoryDTO>> GetAllAsync()
    {
        var response = await _categoryRepository.GetAllAsync();
        var categoriesMapped = _mapper.Map<IEnumerable<CategoryDTO>>(response);

        return new ResponseApiDTO<CategoryDTO> { hasError = false, collectionData = categoriesMapped };
    }

    public async Task<ResponseApiDTO<CategoryDTO>> GetByIdAsync(string id)
    {
        var hasConvertedId = Guid.TryParse(id, out var guidId);

        if (!hasConvertedId) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "ID inválido." };

        var category = await _categoryRepository.GetByIdAsync(guidId);

        if (category == null) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "Essa categoria não existe." };

        var categoryMapped = _mapper.Map<CategoryDTO>(category);

        return new ResponseApiDTO<CategoryDTO> { hasError = false, data = categoryMapped };
    }

    public async Task<ResponseApiDTO<bool>> UpdateAsync(string id, CategoryNewAndUpdateDTO entity)
    {
        if (!Guid.TryParse(id, out Guid guidId)) return new ResponseApiDTO<bool> { hasError = true, message = "ID inválido." };

        var category = await _categoryRepository.GetByIdAsync(guidId);
        if (category == null) return new ResponseApiDTO<bool> { hasError = true, message = "Essa categoria não existe." };

        category.Description = entity.Description;

        await _categoryRepository.UpdateAsync(category);

        return new ResponseApiDTO<bool> { hasError = false };
    }

    //public async Task<ResponseApiDTO<CategoryDTO>> GetByIdAsync(string id)
    //{
    //    var hasConvertedId = Guid.TryParse(id, out var guidId);

    //    if (!hasConvertedId) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "ID inválido." };

    //    var category = await _categoryRepository.GetByIdAsync(guidId);
    //    if (category == null) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "Essa categoria não existe." };

    //    var categoryMapped = _mapper.Map<CategoryDTO>(category);

    //    return new ResponseApiDTO<CategoryDTO> { hasError = false, data = categoryMapped };

    //}

    //public async Task<ResponseApiDTO<CategoryDTO>> UpdateAsync(string id, CategoryDTO entity)
    //{
    //    if (!Guid.TryParse(id, out Guid guidId)) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "ID inválido." };

    //    var category = await _categoryRepository.GetByIdAsync(guidId);
    //    if (category == null) return new ResponseApiDTO<CategoryDTO> { hasError = true, message = "Essa categoria não existe." };

    //    category.Description = entity.Description;

    //    await _categoryRepository.UpdateAsync(category);

    //    return new ResponseApiDTO<CategoryDTO> { hasError = false };
    //}
}
