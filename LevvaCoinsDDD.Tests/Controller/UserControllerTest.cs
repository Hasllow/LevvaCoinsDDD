using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.Tests.Controller;
public class UserControllerTest
{
    private readonly IUserService _userService;
    private readonly UserController _userController;

    public UserControllerTest()
    {
        _userService = A.Fake<IUserService>();
        _userController = new UserController(_userService);
    }

    [Fact(DisplayName = nameof(UserController_GetAllAsync_ReturnOk))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_GetAllAsync_ReturnOk()
    {
        // Arrange

        // Act
        var result = await _userController.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));

    }

    [Fact(DisplayName = nameof(UserController_GetByIdAsync_ReturnOK))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_GetByIdAsync_ReturnOK()
    {
        // Arrange
        var fakeUserId = new Guid();
        var fakeUserIdArgument = new Guid("54c30c07-405f-4103-9e4a-76d5479e5689");

        A.CallTo(() => _userService.GetByIdAsync(A<Guid>.Ignored))
            .Invokes((Guid id) => { fakeUserId = id; });

        // Act
        var result = await _userController.GetByIdAsync(fakeUserIdArgument);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));
        fakeUserId.Should().Be(fakeUserIdArgument);
    }

    [Fact(DisplayName = nameof(UserController_CreateAsync_ReturnCreated))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_CreateAsync_ReturnCreated()
    {
        // Arrange
        var fakeUser = A.Fake<UserDTO>();

        // Act
        var result = await _userController.CreateAsync(fakeUser);

        // Assert
        result.As<CreatedResult>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(UserController_DeleteAsync_ReturnNoContent))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_DeleteAsync_ReturnNoContent()
    {
        // Arrange
        var userDeleted = false;

        A.CallTo(() => _userService.DeleteAsync(A<Guid>.Ignored))
            .Invokes(() => { userDeleted = true; });

        // Act
        var result = await _userController.DeleteAsync(Guid.NewGuid());

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        userDeleted.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(UserController_UpdateAsync_ReturnNoContent))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_UpdateAsync_ReturnNoContent()
    {
        // Arrange
        var fakeUser = A.Fake<UserUpdateDTO>();
        var fakeUserId = Guid.NewGuid();

        A.CallTo(() => _userService.UpdateAsync(A<UserUpdateDTO>.Ignored))
            .Invokes(() =>
            {
                fakeUser.Id = fakeUserId;
            });

        // Act
        var result = await _userController.UpdateAsync(fakeUser);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeUser);
        fakeUser.Id.Should().Be(fakeUserId);
    }

    [Fact(DisplayName = nameof(UserController_Login_ReturnOK))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_Login_ReturnOK()
    {
        // Arrange
        var fakeLogin = A.Fake<LoginDTO>();

        // Act
        var result = await _userController.Login(fakeLogin);

        // Assert
        result.As<OkObjectResult>().Should().BeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        A.CallTo(() => _userService.Login(A<LoginDTO>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
