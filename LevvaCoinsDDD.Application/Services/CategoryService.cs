using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
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

    public async Task CreateAsync(CategoryDTO entity)
    {
        var categoryToCreate = _mapper.Map<Category>(entity);
        categoryToCreate.Id = Guid.NewGuid();
        await _categoryRepository.CreateAsync(categoryToCreate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<CategoryDTO>>(await _categoryRepository.GetAllAsync());
    }

    public async Task<CategoryDTO> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CategoryDTO>(await _categoryRepository.GetByIdAsync(id));
    }

    public async Task UpdateAsync(CategoryUpdateDTO entity)
    {
        await _categoryRepository.UpdateAsync(_mapper.Map<Category>(entity));
    }
}
