using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.Tests.Controller;
public class CategoryControllerTest
{
    private readonly ICategoryService _categoryService;
    private readonly CategoryController _categoryController;

    public CategoryControllerTest()
    {
        _categoryService = A.Fake<ICategoryService>();
        _categoryController = new CategoryController(_categoryService);
    }

    [Fact(DisplayName = nameof(CategoryController_GetAllAsync_ReturnOk))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_GetAllAsync_ReturnOk()
    {
        // Arrange

        // Act
        var result = await _categoryController.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));

    }

    [Fact(DisplayName = nameof(CategoryController_GetByIdAsync_ReturnOK))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_GetByIdAsync_ReturnOK()
    {
        // Arrange
        var fakeCategoryId = new Guid();
        var fakeCategoryIdArgument = new Guid("54c30c07-405f-4103-9e4a-76d5479e5689");

        A.CallTo(() => _categoryService.GetByIdAsync(A<Guid>.Ignored))
            .Invokes((Guid id) => { fakeCategoryId = id; });

        // Act
        var result = await _categoryController.GetByIdAsync(fakeCategoryIdArgument);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));
        fakeCategoryId.Should().Be(fakeCategoryIdArgument);
    }

    [Fact(DisplayName = nameof(CategoryController_CreateAsync_ReturnCreated))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_CreateAsync_ReturnCreated()
    {
        // Arrange
        var fakeCategory = A.Fake<CategoryDTO>();

        // Act
        var result = await _categoryController.CreateAsync(fakeCategory);

        // Assert
        result.As<CreatedResult>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(CategoryController_DeleteAsync_ReturnNoContent))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_DeleteAsync_ReturnNoContent()
    {
        // Arrange
        var categoryDeleted = false;

        A.CallTo(() => _categoryService.DeleteAsync(A<Guid>.Ignored))
            .Invokes(() => { categoryDeleted = true; });

        // Act
        var result = await _categoryController.DeleteAsync(Guid.NewGuid());

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        categoryDeleted.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(CategoryController_UpdateAsync_ReturnNoContent))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_UpdateAsync_ReturnNoContent()
    {
        // Arrange
        var fakeCategory = A.Fake<CategoryUpdateDTO>();
        var fakeCategoryId = Guid.NewGuid();

        A.CallTo(() => _categoryService.UpdateAsync(A<CategoryUpdateDTO>.Ignored))
            .Invokes(() =>
            {
                fakeCategory.Id = fakeCategoryId;
            });

        // Act
        var result = await _categoryController.UpdateAsync(fakeCategory);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeCategory);
        fakeCategory.Id.Should().Be(fakeCategoryId);
    }
}
