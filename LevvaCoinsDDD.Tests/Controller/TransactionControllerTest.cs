using FakeItEasy;
using FluentAssertions;
using LevvaCoinsDDD.API.Controllers;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
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


        var token = "Bearer teste";
        var mockHttpContext = new DefaultHttpContext
        {
            RequestServices = A.Fake<IServiceProvider>()
        };
        mockHttpContext.Request.Headers.Add("Authorization", token);
        _transactionController.ControllerContext = new ControllerContext
        {
            HttpContext = mockHttpContext
        };


    }

    [Fact(DisplayName = nameof(TransactionController_GetAllAsync_ReturnOk))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_GetAllAsync_ReturnOk()
    {
        // Arrange
        var fakeResponse = A.Fake<ResponseApiDTO<TransactionResponseByUserDTO>>();

        A.CallTo(() => _transactionService.GetAllAsync(A<string>.Ignored)).Returns(fakeResponse);

        // Act
        var result = await _transactionController.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType(typeof(OkObjectResult));

    }

    //[Fact(DisplayName = nameof(TransactionController_GetByIdAsync_ReturnOK))]
    //[Trait("Controller", "Transaction - Controller")]
    //public async void TransactionController_GetByIdAsync_ReturnOK()
    //{
    //    // Arrange
    //    var fakeTransactionId = new Guid();
    //    var fakeTransactionIdArgument = "54c30c07-405f-4103-9e4a-76d5479e5689";

    //    A.CallTo(() => _transactionService.GetByIdAsync(A<string>.Ignored, A<string>.Ignored))
    //        .Invokes((string id, string token) => { fakeTransactionId = Guid.Parse(id); });

    //    // Act
    //    var result = await _transactionController.GetByIdAsync(fakeTransactionIdArgument);

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Result.Should().BeOfType(typeof(OkObjectResult));
    //    fakeTransactionId.Should().Be(fakeTransactionIdArgument);
    //}

    [Fact(DisplayName = nameof(TransactionController_CreateAsync_ReturnCreated))]
    [Trait("Controller", "Transaction - Controller")]
    public async void TransactionController_CreateAsync_ReturnCreated()
    {
        // Arrange
        var fakeResponse = A.Fake<ResponseApiDTO<TransactionResponseByUserDTO>>();
        fakeResponse.data = A.Fake<TransactionResponseByUserDTO>();
        var fakeTransaction = A.Fake<TransactionNewDTO>();

        fakeTransaction.Description = "Teste";
        fakeTransaction.Amount = 10;
        fakeTransaction.CategoryID = Guid.NewGuid().ToString();

        A.CallTo(() => _transactionService.CreateAsync(A<TransactionNewDTO>.Ignored, A<string>.Ignored)).Returns(fakeResponse);

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

        A.CallTo(() => _transactionService.DeleteAsync(A<string>.Ignored, A<string>.Ignored))
            .Invokes(() => { transactionDeleted = true; });

        // Act
        var result = await _transactionController.DeleteAsync(Guid.NewGuid().ToString());

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

        fakeTransaction.Description = "Teste";
        fakeTransaction.Amount = 10;
        fakeTransaction.CategoryID = Guid.NewGuid().ToString();
        fakeTransaction.UserID = Guid.NewGuid().ToString();
        fakeTransaction.Date = new DateTime();

        var fakeTransactionDescription = "Teste";

        A.CallTo(() => _transactionService.UpdateAsync(A<string>.Ignored, A<TransactionUpdateDTO>.Ignored, A<string>.Ignored))
            .Invokes(() =>
            {
                fakeTransaction.Description = fakeTransactionDescription;
            });

        // Act
        var result = await _transactionController.UpdateAsync(Guid.NewGuid().ToString(), fakeTransaction);

        // Assert
        result.As<NoContentResult>().Should().NotBeNull();
        result.As<NoContentResult>().Should().NotBeSameAs(fakeTransaction);
        fakeTransaction.Description.Should().Be(fakeTransactionDescription);
    }
}
