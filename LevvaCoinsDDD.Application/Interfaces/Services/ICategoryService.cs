using LevvaCoinsDDD.Application.Dtos.Category;

namespace LevvaCoinsDDD.Application.Interfaces.Services;
public interface ICategoryService : IService<CategoryNewAndUpdateDTO, CategoryDTO,
    CategoryNewAndUpdateDTO, CategoryDTO, bool, CategoryDTO, bool>
{
}
