using AutoFixture;
using AutoFixture.Xunit2;
using DTO;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_Controllers_UniTests;

public class AuthControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IJwtService> _jwtMock;
    private readonly AuthController _sut;
    private Mock<IUserAccountService> _accountServiceMock;
   

    public AuthControllerTests()
    {
        _fixture = new Fixture();
    }

    
    //[Fact]
    //public async Task SignupAccountAsync_WhenNewUser_CreateNewUser()
    //{
        
    //    var jwtMock = new Mock<IJwtService>();

    //    jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
    //        .Returns("token");

    //    var tuple = Tuple.Create<bool, UserAccount>(true, CreateUser());

    //    var userAccountMock = new Mock<IUserAccountService>();
    //    userAccountMock.Setup(x =>
    //        x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).Returns((bool, UserAccount), tuple);
    //    var _sut = new AuthController(GetUser(), _jwtMock.Object);
    //    var response = _sut.SignUpAsync(CreateUserSignUp());
    //    var actual = await response as ObjectResult;
        
    //    Assert.NotNull(actual);
    //    Assert.Equal(201, actual!.StatusCode);
    //}

    //public IUserAccountService GetUser()
    //{
    //    var userAccountItems = new Mock<IDbRepository>();
    //    userAccountItems.Setup(x => x.GetAccountByUserNameAsync(It.IsAny<string>()))
    //        .ReturnsAsync(CreateUser());
    //    userAccountItems.Setup(x => x.UpdateNameAsync(It.IsAny<int>(), It.IsAny<string>()))
    //        .ReturnsAsync(true);
    //    var sut = new UserAccountService(userAccountItems.Object);
    //    return sut;
    //}


    //public SignUpDto CreateUserSignUp()
    //{
    //    var user = new SignUpDto
    //    {
    //        UserName = "GSK",
    //        PersonalId = "123",
    //        Name = "Alvydas",
    //        Surname = "Kazkas",
    //        PhoneNumber = "6845132",
    //        Email = "testas@testas.lt",
    //        Address = new AddressDto
    //            { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 },
    //        Picture = new FormFile(Stream.Null, 20, 200, "name", "jpg")
    //    };
    //    return user;
    //}


    //public UserAccount CreateUser()
    //{
    //    var user = new UserAccount
    //    {
    //        Id = 1,
    //        Role = "User",
    //        UserName = "GSK",
    //        HumanInfo = new HumanInfo
    //        {
    //            PersonalId = "123",
    //            Name = "Alvydas",
    //            Surname = "Kazkas",
    //            PhoneNumber = "6845132",
    //            Email = "testas@testas.lt",
    //            Address = new Address
    //                { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 }
    //        }
    //    };
    //    return user;
    //}
}

