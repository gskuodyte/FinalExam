using API_Controllers_UniTests.Common;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_Controllers_UniTests;

public class AdminControllerTests
{
    private readonly Mock<IUserAccountService> _accountServiceMock;
    private readonly ClaimId _claimId;
    private readonly AdminController _sut;

    public AdminControllerTests()
    {
        _accountServiceMock = new Mock<IUserAccountService>();
        _sut = new AdminController(_accountServiceMock.Object);
        _claimId = new ClaimId(_sut);
    }

    [Fact]
    public async Task DeleteUserById_WhenInputIsEmpty_ReturnsBedRequest()
    {
        var result = await _sut.DeleteUserById(string.Empty);
        var badRequestResult = result as BadRequestObjectResult;

        Assert.Equal(400, badRequestResult.StatusCode);
    }


    [Fact]
    public async Task GetUser_WhenIdIsEmpty_ReturnsBadRequest()
    {
        var result = await _sut.GetUserById(string.Empty);
        var resultAsBadRequest = result.Result as BadRequestObjectResult;

        Assert.Equal(400, resultAsBadRequest.StatusCode);
    }

    [Fact]
    public async Task GetPicture_WhenIdIsEmpty_ReturnsBadRequest()
    {
        var result = await _sut.GetUserImage(string.Empty);
        var resultAsBadRequest = result as BadRequestObjectResult;

        Assert.Equal(400, resultAsBadRequest.StatusCode);
    }
}