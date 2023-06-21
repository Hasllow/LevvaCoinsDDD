using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos;
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
        var fakeCategoryIdArgument = "54c30c07-405f-4103-9e4a-76d5479e5689";

        A.CallTo(() => _categoryService.GetByIdAsync(A<string>.Ignored))
            .Invokes((string id) => { fakeCategoryId = Guid.Parse(id); });

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
        var fakeCategory = A.Fake<CategoryNewAndUpdateDTO>();
        fakeCategory.Description = "Description";
        var fakeResponse = A.Fake<ResponseApiDTO<CategoryDTO>>();

        fakeResponse.data = new CategoryDTO { Id = "Teste", Description = "Description" };

        A.CallTo(() => _categoryService.CreateAsync(A<CategoryNewAndUpdateDTO>.Ignored)).Returns(fakeResponse);

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

        A.CallTo(() => _categoryService.DeleteAsync(A<string>.Ignored))
            .Invokes(() => { categoryDeleted = true; });

        // Act
        var result = await _categoryController.DeleteAsync(Guid.NewGuid().ToString());

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        categoryDeleted.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(CategoryController_UpdateAsync_ReturnNoContent))]
    [Trait("Controller", "Category - Controller")]
    public async void CategoryController_UpdateAsync_ReturnNoContent()
    {
        // Arrange
        var fakeCategory = A.Fake<CategoryNewAndUpdateDTO>();
        var fakeCategoryDescription = "Teste";

        A.CallTo(() => _categoryService.UpdateAsync(A<string>.Ignored, A<CategoryNewAndUpdateDTO>.Ignored))
            .Invokes(() =>
            {
                fakeCategory.Description = fakeCategoryDescription;
            });

        // Act
        var result = await _categoryController.UpdateAsync(Guid.NewGuid().ToString(), fakeCategory);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeCategory);
        fakeCategory.Description.Should().Be(fakeCategoryDescription);
    }
}
