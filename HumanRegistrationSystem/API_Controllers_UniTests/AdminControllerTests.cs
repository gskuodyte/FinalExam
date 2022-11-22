using API_Controllers_UniTests.Common;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using DTO;

namespace API_Controllers_UniTests;

public class AdminControllerTests
{
    private readonly Mock<IUserAccountService> _accountServiceMock;
    private readonly AdminController _sut;

    public AdminControllerTests()
    {
        _accountServiceMock = new Mock<IUserAccountService>();
        _sut = new AdminController(_accountServiceMock.Object);
    }

    [Fact]
    public async Task DeleteUserById_WhenInputIsEmpty_ReturnsBedRequest()
    {
        var result = await _sut.DeleteUserById(string.Empty);
        var badRequestResult = result as BadRequestObjectResult;

        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task DeleteUserById_WhenInputIsGood_ReturnsResultOk()
    {
        var userAccountMock = new Mock<IUserAccountService>();
        var sut = new AdminController(userAccountMock.Object);

        var result = await sut.DeleteUserById("1");
        var okResult = result as OkResult;
        userAccountMock.Verify(x => x.GetMappedUserAccountAsync(1), Times.Never);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetUser_WhenIdIsEmpty_ReturnsBadRequest()
    {
        var result = await _sut.GetUserById(string.Empty);
        var resultAsBadRequest = result.Result as BadRequestObjectResult;

        Assert.Equal(400, resultAsBadRequest.StatusCode);
    }

    [Fact]
    public async Task GetUser_WhenInputIsGood_ReturnsFile()
    {
        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x => x.GetMappedUserAccountAsync(1))
            .ReturnsAsync(user);

        var sut = new AdminController(userAccountMock.Object);

        var result =  await sut.GetUserById("1");
        var objResult = result.Result as OkObjectResult;
        userAccountMock.Verify(x => x.GetMappedUserAccountAsync(1), Times.Once);
        Assert.Equal(user, objResult.Value);
    }

    [Fact]
    public async Task GetPicture_WhenIdIsEmpty_ReturnsBadRequest()
    {
        var result = await _sut.GetUserImage(string.Empty);
        var resultAsBadRequest = result as BadRequestObjectResult;

        Assert.Equal(400, resultAsBadRequest.StatusCode);
    }

    [Fact]
    public async Task GetPicture_WhenInputIsGood_ReturnsFile()
    {
        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x => x.GetMappedUserAccountAsync(1))
            .ReturnsAsync(CreateUserResponse());

        var sut = new AdminController(userAccountMock.Object);

        var result = await sut.GetUserImage("1");
        var objResult = result as FileContentResult;
        userAccountMock.Verify(x => x.GetMappedUserAccountAsync(1), Times.Once);
        Assert.Equal("image/jpeg", objResult.ContentType);
    }

    public UserAccountInfoResponce CreateUserResponse()
    {
        var user = new UserAccountInfoResponce
        {

            UserName = "test",
            PersonalId = "123",
            Name = "Alvydas",
            Surname = "Kazkas",
            PhoneNumber = "6845132",
            Email = "testas@testas.lt",
            AddressDto = new AddressDto()
            { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 },
            Picture = Array.Empty<byte>()
        };
        return user;
    }

        private UserAccountInfoResponce user = new ()
        {
            UserName = "test",
            PersonalId = "123",
            Name = "Alvydas",
            Surname = "Kazkas",
            PhoneNumber = "6845132",
            Email = "testas@testas.lt",
            AddressDto = new AddressDto()
                { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 },
            Picture = Array.Empty<byte>()
        };

}