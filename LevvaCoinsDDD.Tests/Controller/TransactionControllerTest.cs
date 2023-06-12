using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LevvaCoinsDDD.Tests.Controller;
public class TransactionControllerTest
{
    private readonly ITransactionService _transactionService;
    private readonly TransactionController _transactionController;

    public TransactionControllerTest()
    {
        _transactionService = A.Fake<ITransactionService>();
        _transactionController = new TransactionController(_transactionService);
    }

    [Fact(DisplayName = nameof(TransactionController_GetAllAsync_ReturnOk))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_GetAllAsync_ReturnOk()
    {
        // Arrange

        // Act
        var result = await _transactionController.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));

    }

    [Fact(DisplayName = nameof(TransactionController_GetByIdAsync_ReturnOK))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_GetByIdAsync_ReturnOK()
    {
        // Arrange
        var fakeTransactionId = new Guid();
        var fakeTransactionIdArgument = new Guid("54c30c07-405f-4103-9e4a-76d5479e5689");

        A.CallTo(() => _transactionService.GetByIdAsync(A<Guid>.Ignored))
            .Invokes((Guid id) => { fakeTransactionId = id; });

        // Act
        var result = await _transactionController.GetByIdAsync(fakeTransactionIdArgument);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));
        fakeTransactionId.Should().Be(fakeTransactionIdArgument);
    }

    [Fact(DisplayName = nameof(TransactionController_CreateAsync_ReturnCreated))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_CreateAsync_ReturnCreated()
    {
        // Arrange
        var fakeTransaction = A.Fake<TransactionDTO>();

        // Act
        var result = await _transactionController.CreateAsync(fakeTransaction);

        // Assert
        result.As<CreatedResult>().Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(TransactionController_DeleteAsync_ReturnNoContent))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_DeleteAsync_ReturnNoContent()
    {
        // Arrange
        var transactionDeleted = false;

        A.CallTo(() => _transactionService.DeleteAsync(A<Guid>.Ignored))
            .Invokes(() => { transactionDeleted = true; });

        // Act
        var result = await _transactionController.DeleteAsync(Guid.NewGuid());

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        transactionDeleted.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(TransactionController_UpdateAsync_ReturnNoContent))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_UpdateAsync_ReturnNoContent()
    {
        // Arrange
        var fakeTransaction = A.Fake<TransactionUpdateDTO>();
        var fakeTransactionId = Guid.NewGuid();

        A.CallTo(() => _transactionService.UpdateAsync(A<TransactionUpdateDTO>.Ignored))
            .Invokes(() =>
            {
                fakeTransaction.Id = fakeTransactionId;
            });

        // Act
        var result = await _transactionController.UpdateAsync(fakeTransaction);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeTransaction);
        fakeTransaction.Id.Should().Be(fakeTransactionId);
    }
}
