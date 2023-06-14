using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Application.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace LevvaCoinsDDD.Tests.Services;
public class UserServiceTest
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly UserService _userService;

    public UserServiceTest()
    {
        _userRepository = A.Fake<IUserRepository>();
        _mapper = A.Fake<IMapper>();
        _config = A.Fake<IConfiguration>();
        _userService = new UserService(_userRepository, _mapper, _config);
    }

    [Fact(DisplayName = nameof(UserService_CreateAsync_ReturnVoid))]
    [Trait("Service", "User - Service")]
    public async void UserService_CreateAsync_ReturnVoid()
    {
        // Arrange
        var fakeUserDTO = A.Fake<UserNewAccountDTO>();
        fakeUserDTO.Email = "email@email.com";
        A.CallTo(() => _userRepository.GetByEmailAsync(A<string>.Ignored)).Returns(Task.FromResult<User>(null));

        // Act
        await _userService.CreateAsync(fakeUserDTO);

        // Assert
        A.CallTo(() => _userRepository.CreateAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(UserService_DeleteAsync_ReturnVoid))]
    [Trait("Service", "User - Service")]
    public async void UserService_DeleteAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = Guid.NewGuid().ToString();

        // Act
        await _userService.DeleteAsync(fakeId);

        // Assert
        A.CallTo(() => _userRepository.DeleteAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(UserService_GetAllAsync_ReturnEnumerableUserDTO))]
    [Trait("Service", "User - Service")]
    public async void UserService_GetAllAsync_ReturnEnumerableUserDTO()
    {
        // Arrange

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        A.CallTo(() => _userRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        result.As<ResponseApiDTO<UserDTO>>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(UserService_GetByIdAsync_ReturnUserDTO))]
    [Trait("Service", "User - Service")]
    public async void UserService_GetByIdAsync_ReturnUserDTO()
    {
        // Arrange
        var fakeUser = A.Fake<UserDTO>();
        var fakeId = Guid.NewGuid().ToString();
        A.CallTo(() => _mapper.Map<UserDTO>(A<User>.Ignored)).Returns(fakeUser);

        // Act
        var result = await _userService.GetByIdAsync(fakeId);

        // Assert
        A.CallTo(() => _userRepository.GetByIdAsync(Guid.Parse(fakeId))).MustHaveHappenedOnceExactly();
        result.As<ResponseApiDTO<UserDTO>>().Should().NotBeNull();
        result.data.Should().BeEquivalentTo(fakeUser);
    }

    [Fact(DisplayName = nameof(UserService_GetByIdAsync_ReturnUserDTO))]
    [Trait("Service", "User - Service")]
    public async void UserService_UpdateAsync_ReturnVoid()
    {
        // Arrange
        var fakeUserId = Guid.NewGuid().ToString();

        var fakeUser = A.Fake<UserUpdateDTO>();
        fakeUser.Email = "email@email.com";

        // Act
        await _userService.UpdateAsync(fakeUserId, fakeUser);

        // Assert
        A.CallTo(() => _userRepository.UpdateAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(UserService_Login_ReturnLoginDTO))]
    [Trait("Service", "User - Service")]
    public async void UserService_Login_ReturnLoginDTO()
    {
        // Arrange
        var fakeUser = A.Fake<User>();
        fakeUser.Email = "test@test.com";

        var fakeLogin = A.Fake<LoginDTO>();

        var fakeResponse = A.Fake<LoginValuesDTO>();
        fakeResponse.Name = "Test";

        var fakeConfigSection = A.Fake<IConfigurationSection>();
        fakeConfigSection.Value = "43e4dbf0-52ed-4203-895d-42b586496bd4";

        A.CallTo(() => _config.GetSection(A<string>.Ignored))
                .Returns(fakeConfigSection);

        A.CallTo(() => _userRepository.GetByEmailAndPasswordAsync(A<string>.Ignored, A<string>.Ignored))
            .Returns(fakeUser);

        A.CallTo(() => _mapper.Map<LoginValuesDTO>(A<User>.Ignored)).Returns(fakeResponse);

        // Act
        var result = await _userService.Login(fakeLogin);

        // Assert
        A.CallTo(() => _userRepository.GetByEmailAndPasswordAsync(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        result.data.Email.Should().BeEquivalentTo(fakeLogin.Email);
        result.data.Name.Should().Be("Test");
        result.data.Token.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(UserService_Login_ReturnBadRequest))]
    [Trait("Service", "User - Service")]
    public async void UserService_Login_ReturnBadRequest()
    {
        // Arrange
        User fakeNullUser = null;
        var fakeLogin = A.Fake<LoginDTO>();

        A.CallTo(() => _userRepository.GetByEmailAndPasswordAsync(A<string>.Ignored, A<string>.Ignored))
            .Returns(fakeNullUser);

        // Act
        var result = await _userService.Login(fakeLogin);

        // Assert
        A.CallTo(() => _userRepository.GetByEmailAndPasswordAsync(A<string>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
        result.hasError.Should().BeTrue();
    }
}
