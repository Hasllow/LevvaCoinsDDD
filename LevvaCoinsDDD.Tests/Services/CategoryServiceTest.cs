using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;

namespace LevvaCoinsDDD.Tests.Services;
public class CategoryServiceTest
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly CategoryService _categoryService;

    public CategoryServiceTest()
    {
        _categoryRepository = A.Fake<ICategoryRepository>();
        _mapper = A.Fake<IMapper>();
        _categoryService = new CategoryService(_categoryRepository, _mapper);
    }

    [Fact(DisplayName = nameof(CategoryService_CreateAsync_ReturnVoid))]
    [Trait("Service", "Category - Service")]
    public async void CategoryService_CreateAsync_ReturnVoid()
    {
        // Arrange
        var fakeCategoryDTO = A.Fake<CategoryNewAndUpdateDTO>();

        // Act
        await _categoryService.CreateAsync(fakeCategoryDTO);

        // Assert
        A.CallTo(() => _categoryRepository.CreateAsync(A<Category>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(CategoryService_DeleteAsync_ReturnVoid))]
    [Trait("Service", "Category - Service")]
    public async void CategoryService_DeleteAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = "";

        // Act
        await _categoryService.DeleteAsync(fakeId);

        // Assert
        A.CallTo(() => _categoryRepository.DeleteAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(CategoryService_GetAllAsync_ReturnEnumerableCategoryDTO))]
    [Trait("Service", "Category - Service")]
    public async void CategoryService_GetAllAsync_ReturnEnumerableCategoryDTO()
    {
        // Arrange

        // Act
        var result = await _categoryService.GetAllAsync();

        // Assert
        A.CallTo(() => _categoryRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        result.As<IEnumerable<CategoryDTO>>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(CategoryService_GetByIdAsync_ReturnCategoryDTO))]
    [Trait("Service", "Category - Service")]
    public async void CategoryService_GetByIdAsync_ReturnCategoryDTO()
    {
        // Arrange
        var fakeCategory = A.Fake<CategoryDTO>();
        var fakeId = "";
        A.CallTo(() => _mapper.Map<CategoryDTO>(A<Category>.Ignored)).Returns(fakeCategory);

        // Act
        var result = await _categoryService.GetByIdAsync(fakeId);

        // Assert
        A.CallTo(() => _categoryRepository.GetByIdAsync(Guid.Parse(fakeId))).MustHaveHappenedOnceExactly();
        result.As<CategoryDTO>().Should().NotBeNull();
        result.Should().BeEquivalentTo(fakeCategory);
    }

    [Fact(DisplayName = nameof(CategoryService_GetByIdAsync_ReturnCategoryDTO))]
    [Trait("Service", "Category - Service")]
    public async void CategoryService_UpdateAsync_ReturnVoid()
    {
        // Arrange
        var fakeCategory = A.Fake<CategoryNewAndUpdateDTO>();

        // Act
        await _categoryService.UpdateAsync("", fakeCategory);

        // Assert
        A.CallTo(() => _categoryRepository.UpdateAsync(A<Category>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
