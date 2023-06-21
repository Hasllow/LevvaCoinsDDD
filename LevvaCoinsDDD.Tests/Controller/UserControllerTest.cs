using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.User;
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
        var fakeUserIdArgument = "54c30c07-405f-4103-9e4a-76d5479e5689";

        A.CallTo(() => _userService.GetByIdAsync(A<string>.Ignored))
            .Invokes((string id) => { fakeUserId = Guid.Parse(id); });

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
        var fakeUser = A.Fake<UserNewAccountDTO>();
        fakeUser.Name = "Name";
        fakeUser.Email = "email@email.com";
        fakeUser.Password = "Password";
        fakeUser.ConfirmPassword = "Password";

        var fakeResponse = A.Fake<ResponseApiDTO<UserDTO>>();
        fakeResponse.data = A.Fake<UserDTO>();
        A.CallTo(() => _userService.CreateAsync(A<UserNewAccountDTO>.Ignored)).Returns(fakeResponse);

        // Act
        var result = await _userController.CreateAsync(fakeUser);

        // Assert
        result.Result.As<CreatedResult>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(UserController_DeleteAsync_ReturnNoContent))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_DeleteAsync_ReturnNoContent()
    {
        // Arrange
        var userDeleted = false;

        A.CallTo(() => _userService.DeleteAsync(A<string>.Ignored))
            .Invokes(() => { userDeleted = true; });

        // Act
        var result = await _userController.DeleteAsync(Guid.NewGuid().ToString());

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
        var fakeUserName = "Teste";

        A.CallTo(() => _userService.UpdateAsync(A<string>.Ignored, A<UserUpdateDTO>.Ignored))
            .Invokes(() =>
            {
                fakeUser.Name = fakeUserName;
            });

        // Act
        var result = await _userController.UpdateAsync(Guid.NewGuid().ToString(), fakeUser);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeUser);
        fakeUser.Name.Should().Be(fakeUserName);
    }

    [Fact(DisplayName = nameof(UserController_Login_ReturnBadRequest))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_Login_ReturnBadRequest()
    {
        // Arrange
        var fakeLogin = A.Fake<LoginDTO>();
        fakeLogin.Email = "email@email.com";
        fakeLogin.Password = "Password";
        var fakeResponse = A.Fake<ResponseApiDTO<LoginValuesDTO>>();
        fakeResponse.data = A.Fake<LoginValuesDTO>();
        fakeResponse.hasError = true;

        A.CallTo(() => _userService.Login(A<LoginDTO>.Ignored)).Returns(fakeResponse);

        // Act
        var result = await _userController.Login(fakeLogin);

        // Assert
        A.CallTo(() => _userService.Login(A<LoginDTO>.Ignored)).MustHaveHappenedOnceExactly();
        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact(DisplayName = nameof(UserController_Login_ReturnOK))]
    [Trait("Controller", "User - Controller")]
    public async void UserController_Login_ReturnOK()
    {
        // Arrange
        var fakeLogin = A.Fake<LoginDTO>();
        fakeLogin.Email = "email@email.com";
        fakeLogin.Password = "Password";

        // Act
        var result = await _userController.Login(fakeLogin);

        // Assert
        result.As<OkObjectResult>().Should().BeNull();
        result.Result.Should().BeOfType<OkObjectResult>();

        A.CallTo(() => _userService.Login(A<LoginDTO>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
