using AutoMapper;
using FakeItEasy;
using FluentAssertions;
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

    public TransactionServiceTest()
    {
        _transactionRepository = A.Fake<ITransactionRepository>();
        _mapper = A.Fake<IMapper>();
        _transactionService = new TransactionService(_transactionRepository, _mapper);
    }

    [Fact(DisplayName = nameof(TransactionService_CreateAsync_ReturnVoid))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_CreateAsync_ReturnVoid()
    {
        // Arrange
        var fakeTransactionDTO = A.Fake<TransactionDTO>();

        // Act
        await _transactionService.CreateAsync(fakeTransactionDTO);

        // Assert
        A.CallTo(() => _transactionRepository.CreateAsync(A<Transaction>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(TransactionService_DeleteAsync_ReturnVoid))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_DeleteAsync_ReturnVoid()
    {
        // Arrange
        var fakeId = Guid.NewGuid();

        // Act
        await _transactionService.DeleteAsync(fakeId);

        // Assert
        A.CallTo(() => _transactionRepository.DeleteAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact(DisplayName = nameof(TransactionService_GetAllAsync_ReturnEnumerableTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_GetAllAsync_ReturnEnumerableTransactionDTO()
    {
        // Arrange

        // Act
        var result = await _transactionService.GetAllAsync();

        // Assert
        A.CallTo(() => _transactionRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        result.As<IEnumerable<TransactionDTO>>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(TransactionService_GetByIdAsync_ReturnTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_GetByIdAsync_ReturnTransactionDTO()
    {
        // Arrange
        var fakeTransaction = A.Fake<TransactionDTO>();
        var fakeId = Guid.NewGuid();
        A.CallTo(() => _mapper.Map<TransactionDTO>(A<Transaction>.Ignored)).Returns(fakeTransaction);

        // Act
        var result = await _transactionService.GetByIdAsync(fakeId);

        // Assert
        A.CallTo(() => _transactionRepository.GetByIdAsync(fakeId)).MustHaveHappenedOnceExactly();
        result.As<TransactionDTO>().Should().NotBeNull();
        result.Should().BeEquivalentTo(fakeTransaction);
    }

    [Fact(DisplayName = nameof(TransactionService_GetByIdAsync_ReturnTransactionDTO))]
    [Trait("Service", "Transaction - Service")]
    public async void TransactionService_UpdateAsync_ReturnVoid()
    {
        // Arrange
        var fakeTransaction = A.Fake<TransactionUpdateDTO>();

        // Act
        await _transactionService.UpdateAsync(fakeTransaction);

        // Assert
        A.CallTo(() => _transactionRepository.UpdateAsync(A<Transaction>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
