using System.Net;
using API_Controllers_UniTests.Common;
using AutoFixture;
using DTO;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Configuration;

namespace API_Controllers_UniTests;

public class AuthControllerTests
{
    private readonly IFixture _fixture;

    private readonly AuthController _sut;
    


    public AuthControllerTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new SignUpDtoSpecimenBuilder());
    }


    [Fact]
    public async Task LoginAsync_WhenUserExists_ReturnStatusOK()
    {

        var jwtMock = new Mock<IJwtService>();
        jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
            .Returns("token");
        

        var tuple = (true, CreateUser());

        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x =>
            x.LoginAsync("test", It.IsAny<string>())).ReturnsAsync(tuple);


        var _sut = new AuthController(userAccountMock.Object, jwtMock.Object);

        var response = await _sut.LoginAsync(GetLoginExistingDto());
        var okResult = response as OkObjectResult;

        userAccountMock.Verify(u => u.LoginAsync("test", It.IsAny<string>()), Times.Once);
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task LoginAsync_WhenExistingUserBadPassword_BadRequest()
    {

        var jwtMock = new Mock<IJwtService>();
        jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
            .Returns("token");


        var tuple = (false, CreateUser());

        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x =>
            x.LoginAsync("test", It.IsAny<string>())).ReturnsAsync(tuple);


        var _sut = new AuthController(userAccountMock.Object, jwtMock.Object);

        var response = await _sut.LoginAsync(GetLoginExistingDto());
        var badResult = response as BadRequestObjectResult;

        userAccountMock.Verify(u => u.LoginAsync("test", It.IsAny<string>()), Times.Once);
        Assert.NotNull(badResult);
        Assert.Equal(400, badResult.StatusCode);
    }

    [Fact]
    public async Task LoginAsync_WhenNotExistingUser_BadRequest()
    {

        var jwtMock = new Mock<IJwtService>();
        jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
            .Returns("token");


        (bool authenticationSuccessful, UserAccount? userAccount) tuple = (false, null);

        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x =>
            x.LoginAsync("test", It.IsAny<string>())).ReturnsAsync(tuple);


        var _sut = new AuthController(userAccountMock.Object, jwtMock.Object);

        var response = await _sut.LoginAsync(GetLoginExistingDto());
        var badResult = response as BadRequestObjectResult;

        userAccountMock.Verify(u => u.LoginAsync("test", It.IsAny<string>()), Times.Once);
        Assert.NotNull(badResult);
        Assert.Equal(400, badResult.StatusCode);
    }

    [Fact]
    public async Task SignUpAsync_WhenUserNotSetAllObjectsInSignUp_BadRequest()
    {
        var signUpRequest = _fixture.Create<SignUpDto>();
        var jwtMock = new Mock<IJwtService>();
        jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
            .Returns("token");
        var byteArray = new byte[100000];
        var userAccountMock = new Mock<IUserAccountService>();
        userAccountMock.Setup(x => x.FileUploadAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(byteArray);
        userAccountMock.Setup(x =>
            x.CreateUserAccountAsync(CreateUserSignUp(), byteArray)).ReturnsAsync(true);

        var _sut = new AuthController(userAccountMock.Object, jwtMock.Object);

        var response = await _sut.SignUpAsync(CreateUserSignUp());
        var badResult = response as BadRequestObjectResult;

        userAccountMock.Verify(u => u.FileUploadAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        Assert.Equal(400, badResult.StatusCode);
    }

   

    public LoginDto GetLoginExistingDto()
    {
        var loginDto = new LoginDto
        {
            UserName = "test",
            Password = "test"
        };
        return loginDto;
    }

    public LoginDto GetLoginNotExistingDto()
    {
        var loginDto = new LoginDto
        {
            UserName = "",
            Password = ""
        };
        return loginDto;
    }
    public SignUpDto CreateUserSignUp()
    {
        var ms = new MemoryStream();
        var user = new SignUpDto
        {
            UserName = "GSK",
            Password ="Any",
            PersonalId = "123",
            Name = "Alvydas",
            Surname = "Kazkas",
            PhoneNumber = "6845132",
            Email = "testas@testas.lt",
            Address = new AddressDto
            { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 },
            Picture = new FormFile(ms, 20, 200, "name", "jpg")
        };
        return user;
    }


    public UserAccount CreateUser()
    {
        var user = new UserAccount
        {
            Id = 1,
            Role = "User",
            PasswordHash = Array.Empty<byte>(),
            PasswordSalt = Array.Empty<byte>(),
            UserName = "test",
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

    public UserAccount CreateUserNotExisting()
    {
        var user = new UserAccount
        {
            
        };
        return user;
    }
}

