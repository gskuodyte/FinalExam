using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API_Controllers_UniTests.Common;
using AutoFixture;
using AutoFixture.Xunit2;
using DTO;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_Domain;
using Microsoft.VisualBasic.CompilerServices;
using HumanRegistrationSystem_DAL;
using Microsoft.AspNetCore.Http;

namespace API_Controllers_UniTests
{
    public class AuthControllerTests
    {
        private  Mock<IUserAccountService> _accountServiceMock;
        private readonly Mock<IJwtService> _jwtMock ;
        private readonly AuthController _sut;
        private readonly Fixture _fixture;
        //private readonly ClaimId _claimId;
        
        public AuthControllerTests()
        {
            //_accountServiceMock = new Mock<IUserAccountService>();
            //_sut = new AuthController(_accountServiceMock.Object, _jwtMock.Object);
            _fixture = new Fixture();
            //_claimId = new ClaimId(_sut);
        }

        [Theory, AutoData]
        public async Task SignUp_WhenUserNameIsEmpty_ReturnBadRequest(SignUpDto signUpDto)
        {
            
            var result = await _sut.SignUpAsync(signUpDto);
            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
        }


        //[Fact]
        //public async Task Login_WhenUserNotExists_ResponseNotFound()
        //{
        //    _jwtMock
        //        .Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
        //        .Returns("token");
        //    var user = _fixture.Create<LoginDto>();
        //    var response = _sut.LoginAsync(user).Result();


        //    var actual = await response as ObjectResult;
        //    Assert.Equal(400, actual!.StatusCode);
        //}


        [Fact]
        public async Task SignupAccountAsync_WhenNewUser_CreateNewUser()
        {
            var jwtMock = new Mock<IJwtService>();

            jwtMock.Setup(j => j.GetJwtToken(It.IsAny<UserAccount>()))
                .Returns("token");

            //var accountMock = new Mock<IUserAccountService>();

            //accountMock = GetUser();



           

            var _sut = new AuthController(GetUser(), _jwtMock.Object);


            var response =  _sut.SignUpAsync(CreateUserSignUp());
            //var viewResult = Assert.IsType<ViewResult>(actual);
            
           

            //Assert.Equal("200", viewResult);
                
            //var sut1 = GetUse
            //r();

            //_accountServiceMock
            //    .Setup(s => s.CreateUserAccountAsync(signupRequest, It.IsAny<byte[]>()))
            //    .ReturnsAsync(true);

            //var response = _sut.SignUpAsync(CreateUserSignUp());
            var actual = await response as ObjectResult;

            Assert.NotNull(actual);
            Assert.Equal(201, actual!.StatusCode);

            //_accountServiceMock.Verify(m =>
            //    m.SignupAccountAsync(
            //        signupRequest.LoginName,
            //        signupRequest.Password,
            //        It.IsAny<UserInfoDto>()
            //    ), Times.Once);
        }

        public IUserAccountService GetUser()
        {
            
            var userAccountItems = new Mock<IDbRepository>();
            userAccountItems.Setup(x => x.GetAccountByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(CreateUser());
            userAccountItems.Setup(x => x.UpdateName(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            var sut = new UserAccountService(userAccountItems.Object);
            return sut;
        }

        

        
       

        public SignUpDto CreateUserSignUp()
        {
            var user = new SignUpDto()
            {
                UserName = "GSK",
                PersonalID = 123,
                Name = "Alvydas",
                Surname = "Kazkas",
                PhoneNumber = "6845132",
                Email = "testas@testas.lt",
                Address = new AddressDto
                        { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 },
                Picture = new FormFile(Stream.Null, 20, 200, "name", "jpg")
            };
            return user;
        }

        public UserAccount CreateUser()
        {
            var user = new UserAccount()
            {
                Id = 1,
                Role = "User",
                UserName = "GSK",
                HumanInfo = new HumanInfo
                {
                    PersonalId = 123,
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
    























    //[Fact]
    //public async Task RegisterUser_WhenResultIsNotSuccess_ReturnsBadRequest()
    //{
    //    var userRequestMock = _fixture.Build<LoginDto>().Create();
    //    var userResponseMock = _accountServiceMock
    //        .Setup(x =>
    //            x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
    //        .ReturnsAsync(,  new UserAccount());

    //    var result = await _sut.Login(userRequestMock);
    //    var resultAsBadRequest = result as BadRequestObjectResult;

    //    resultAsBadRequest.StatusCode.Should().Be(400);
    //}

    //[Fact]
    //public async Task RegisterUser_WhenResultIsSuccess_ReturnsOk()
    //{
    //    var userRequestMock = _fixture.Build<LoginDto>().Create();

    //    _accountServiceMock
    //        .Setup(x =>
    //            x.CreateUserAccountAsync(userRequestMock.UserName, userRequestMock.Password))
    //        .ReturnsAsync(new AuthorizationResult<UserAccount> { IsSuccess = true });

    //    var result = await _sut.RegisterUser(userRequestMock);
    //    var resultAsOkResult = result as OkObjectResult;

    //    resultAsOkResult.StatusCode.Should().Be(200);
    //}

    //[Fact]
    //public async Task LoginUser_WhenResultIsNotSuccess_ReturnsBadRequest()
    //{
    //    var userRequestMock = _fixture.Build<LoginDto>().Create();

    //    _accountServiceMock
    //        .Setup(x =>
    //            x.LoginUserAsync(userRequestMock.UserName, userRequestMock.Password))
    //        .ReturnsAsync(new AuthorizationResult<UserAccount> { IsSuccess = true });

    //    var result = await _sut.LoginUser(userRequestMock);
    //    var resultAsBadRequest = result as BadRequestObjectResult;

    //    resultAsBadRequest.StatusCode.Should().Be(400);
    //}

    //[Fact]
    //public async Task LoginUser_WhenResultIsSuccess_ReturnsOk()
    //{
    //    var userRequestMock = _fixture.Build<LoginDto>().Create();

    //    _accountServiceMock
    //        .Setup(x =>
    //            x.LoginAsync(userRequestMock.UserName, userRequestMock.Password))
    //        .ReturnsAsync(new AuthorizationResult<UserAccount> { IsSuccess = true });

    //    var result = await _sut.LoginUser(userRequestMock);
    //    var resultAsOkResult = result as OkObjectResult;

    //    resultAsOkResult.StatusCode.Should().Be(200);
    //}

    //[Theory, CreatePersonRequestMock]
    //public async void CreatePerson_WhenUserIsNotAdminAndIdIsNotValid_ReturnsBadRequest(
    //    CreatePersonRequest createPersonRequest)
    //{
    //    Person person = _fixture.Create<UserAccount>();

    //    _claimId.SeedUserMockRole();

    //    var result = await _sut.CreatePerson(createPersonRequest, "1");
    //    var badRequestResult = result as BadRequestObjectResult;

    //    badRequestResult.StatusCode.Should().Be(400);
    //    _userServiceMock.Verify(a => a.AddPersonAsync(person), Times.Never);

    //}

    //[Fact]
    //public async Task SignupAccountAsync_WhenNewUser_CreateNewUser()
    //{
    //    var signupRequest = _fixture.Create<SignUpDto>();

    //    _accountServiceMock
    //        .Setup(s => s.CreateUserAccountAsync(signupRequest, It.IsAny<byte[]>()))
    //        .ReturnsAsync( );

    //    var response = _sut.SignUpAsync(signupRequest);
    //    var actual = await response as ObjectResult;

    //    Assert.NotNull(actual);
    //    Assert.Equal(201, actual!.StatusCode);

    //    _accontServiceMock.Verify(m =>
    //        m.SignupAccountAsync(
    //            signupRequest.LoginName,
    //            signupRequest.Password,
    //            It.IsAny<UserInfoDto>()
    //        ), Times.Once);
    //}
}

