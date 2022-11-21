using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_Controllers_UniTests;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly Mock<IUserAccountService> _userServiceMock;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserAccountService>();
        _sut = new UserController(_userServiceMock.Object);
    }


    [Fact]
    public async Task UpdateName_WhenUserIdIsNotValidAndNotAdmin_ReturnsBadRequest()
    {
        var result = await _sut.UpdateHumanNameAsync(" ");
        var resultAsBadRequest = result as BadRequestObjectResult;

        Assert.Equal(400, resultAsBadRequest.StatusCode);
    }

    [Fact]
    public async Task UpdateUsersPersonalId_WhenInputIsZero_ReturnsBadRequest()
    {
        var result = await _sut.UpdateHumanPersonalIdAsync(string.Empty);
        var badRequestResult = result as BadRequestObjectResult;

        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUsersPersonalId_WhenServiceThrowsNullReferenceException_ReturnsInternalServerError()
    {
        _userServiceMock.Setup(p => p.UpdateUserPersonalIdAsync(2, It.IsAny<string>()))
            .ThrowsAsync(new NullReferenceException());
        var result = await _sut.UpdateHumanPersonalIdAsync("3");
        var resultAsStatusCode = result as StatusCodeResult;

        Assert.Equal(500, resultAsStatusCode.StatusCode);
    }

    [Fact]
    public async Task UpdateUsersName_WhenUserValid_ReturnsTrue()
    {
        var sut = GetUser();
        var data = await sut.UpdateUserNameAsync(1, "Petras");
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUsersNameFails_WhenUserNotValid_ReturnsFalse()
    {
        var sut = GetNewUser();
        var data = await sut.UpdateUserNameAsync(2, "Petras");
        Assert.Equal(false, data);
    }

    [Fact]
    public IUserAccountService GetUser()
    {
        var userAccountItems = new Mock<IDbRepository>();
        userAccountItems.Setup(x => x.GetUserByIdAsync(1))
            .ReturnsAsync(CreateUser());
        userAccountItems.Setup(x => x.UpdateNameAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        var sut = new UserAccountService(userAccountItems.Object);
        return sut;
    }

    public IUserAccountService GetNewUser()
    {
        var userAccountItems = new Mock<IDbRepository>();
        userAccountItems.Setup(x => x.GetUserByIdAsync(1))
            .ReturnsAsync(CreateUser());
        userAccountItems.Setup(x => x.UpdateNameAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        var sut = new UserAccountService(userAccountItems.Object);
        return sut;
    }

    public UserAccount CreateUser()
    {
        var user = new UserAccount
        {
            Id = 1,
            Role = "User",
            UserName = "GSK",
            HumanInfo = new HumanInfo
            {
                PersonalId = "123",
                Name = "Alvydas",
                Surname = "Kazkas",
                PhoneNumber = "6845132",
                Email = "testas@testas.lt",
                Address = new Address
                    { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 }
            }
        };
        return user;
    }
}