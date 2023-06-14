using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Services;
using LevvaCoinsDDD.Domain.Models;
using LevvaCoinsDDD.Infra.Data.Interfaces.Repositories;

namespace LevvaCoinsDDD.Tests.Services;
public class TransactionServiceTest
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly TransactionService _transactionService;
    private readonly string _token;

    public TransactionServiceTest()
    {
        _transactionRepository = A.Fake<ITransactionRepository>();
        _mapper = A.Fake<IMapper>();
        _transactionService = new TransactionService(_transactionRepository, _mapper);
        _token = $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbC" +
            $"I6Impob24uZG9lQGxldnZhLmlvIiwibmFtZWlkIjoiMDAwMDAwMDAtMDAwMC0w" +
            $"MDAwLTAwMDAtMDAwMDAwMDAwMDAwIiwibmJmIjoxNjg2NjIwMDY2LCJleHAiOj" +
            $"E2ODY2MjM2NjYsImlhdCI6MTY4NjYyMDA2Nn0.QKtU5i69WmLukZE1-Vb6uGLT" +
            $"gUEwbuCpTO7F17FR6uE";
    }

    [Fact(DisplayName = nameof(TransactionService_CreateAsync_ReturnVoid))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_CreateAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = Guid.NewGuid().ToString();
        var fakeTransactionDTO = A.Fake<TransactionNewDTO>();

        // Act
        await _transactionService.CreateAsync(fakeTransactionDTO, _token);

        // Assert
        A.CallTo(() => _transactionRepository.CreateAndReturnAsync(A<Transaction>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(TransactionService_DeleteAsync_ReturnVoid))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_DeleteAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = Guid.NewGuid().ToString();

        // Act
        await _transactionService.DeleteAsync(fakeId, _token);

        // Assert
        A.CallTo(() => _transactionRepository.DeleteAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(TransactionService_GetAllAsync_ReturnEnumerableTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_GetAllAsync_ReturnEnumerableTransactionDTO()
    {
        // Arrange

        // Act
        var result = await _transactionService.GetAllAsync(_token);

        // Assert
        A.CallTo(() => _transactionRepository.GetByUserIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
        result.As<ResponseApiDTO<TransactionResponseByUserDTO>>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(TransactionService_GetByIdAsync_ReturnTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_GetByIdAsync_ReturnTransactionDTO()
    {
        // Arrange
        var fakeTransaction = A.Fake<TransactionResponseByUserDTO>();
        var fakeId = Guid.NewGuid().ToString();
        A.CallTo(() => _mapper.Map<TransactionResponseByUserDTO>(A<Transaction>.Ignored)).Returns(fakeTransaction);

        // Act
        var result = await _transactionService.GetByIdAsync(fakeId, _token);

        // Assert
        A.CallTo(() => _transactionRepository.GetByIdAsync(Guid.Parse(fakeId))).MustHaveHappenedOnceExactly();
        result.As<ResponseApiDTO<TransactionResponseByUserDTO>>().Should().NotBeNull();
        result.data.Should().BeEquivalentTo(fakeTransaction);
    }

    [Fact(DisplayName = nameof(TransactionService_GetByIdAsync_ReturnTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_UpdateAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = Guid.NewGuid().ToString();
        var fakeTransaction = A.Fake<TransactionUpdateDTO>();

        // Act
        await _transactionService.UpdateAsync(fakeId, fakeTransaction, _token);

        // Assert
        A.CallTo(() => _transactionRepository.UpdateAsync(A<Transaction>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
