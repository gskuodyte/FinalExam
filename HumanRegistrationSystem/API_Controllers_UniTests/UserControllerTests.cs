using System.Linq.Expressions;
using API_Controllers_UniTests.Common;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using HumanRegistrationSystem_Domain;
using DTO;
using HumanRegistrationSystem_DAL;

namespace API_Controllers_UniTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserAccountService> _userServiceMock;
        private readonly UserController _sut;
        private readonly IFixture _fixture;
        private readonly ClaimId _claimId;
        private  List<UserAccount> _users;
        private readonly Mock<IDbRepository> _dbRepositoryMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserAccountService>();
            _sut = new UserController(_userServiceMock.Object);
            _fixture = new Fixture();
            //_fixture.Customizations.Add(new PersonModelMockSpecimenBuilder());
            _claimId = new ClaimId(_sut);
            _dbRepositoryMock = new Mock<IDbRepository>();
            _users = new List<UserAccount>();
        }



        [Fact]
        public async Task UpdateName_WhenUserIdIsNotValidAndNotAdmin_ReturnsBadRequest()
        {
            var sut = GetUser();
            var result = await _sut.UpdateHumanName(" ");
            var resultAsBadRequest = result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
            
        }
        [Fact]
        public async Task UpdateUsersPersonalId_WhenInputIsZero_ReturnsBadRequest()
        {
            var result = await _sut.UpdateHumanPersonalId(string.Empty);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }
        [Fact]
        public async Task UpdateUsersPersonalId_WhenServiceThrowsNullReferenceException_ReturnsInternalServerError()
        {
            
            _userServiceMock.Setup(p => p.UpdateUserPersonalId(2, It.IsAny<int>()))
                .ThrowsAsync(new NullReferenceException());
            var result = await _sut.UpdateHumanPersonalId("3");
            var resultAsStatusCode = result as StatusCodeResult;

            Assert.Equal(500, resultAsStatusCode.StatusCode);
        }
        [Fact]
        public async Task UpdateUsersName_WhenInputIsZero_ReturnsBadRequest()
        {
            var sut = GetUser();
            var data = await sut.UpdateUserName(1, "Petras");
            Assert.Equal(true, data);
        }

        [Fact]
        public async Task UpdateUsersNameFails_WhenInputIsZero_ReturnsBadRequest()
        {
            var sut = GetUser();
            var data = await sut.UpdateUserName(2, "Petras");
            Assert.Equal(false, data);
        }

        [Fact]

        public IUserAccountService GetUser()
        {
            
            var userAccountItems = new Mock<IDbRepository>();
            userAccountItems.Setup(x => x.GetUserByIdAsync(1))
                .ReturnsAsync(CreateUser());
            userAccountItems.Setup(x => x.UpdateName(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            var sut = new UserAccountService(userAccountItems.Object);
            return sut;
        }
        //public IUserAccountService GetAdmin()
        //{

        //    var userAccountItems = new Mock<IDbRepository>();
        //    userAccountItems.Setup(x => x.GetUserByIdAsync(2))
        //        .ReturnsAsync(CreateAdmin);
        //    var sut = new UserAccountService(userAccountItems.Object);
        //    return sut;
        //}

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

        //public UserAccount CreateAdmin()
        //{
        //    var user = new UserAccount()
        //    {
        //        Id = 2,
        //        Role = "Admin",
        //        UserName = "KSG",
        //        HumanInfo = new HumanInfo
        //        {
        //            PersonalId = 321,
        //            Name = "Totoro",
        //            Surname = "Vytas",
        //            PhoneNumber = "56841",
        //            Email = "testas2@testas.lt",
        //            Address = new Address
        //                { City = "Kaunas", Street = "Pusu", HouseNumber = 5, ApartmentNumber = 5 }
        //        }
        //    };
        //    return user;
        //}
        public async Task CreateUsers()
        {
            _users = new List<UserAccount>
            {
                new()
                {
                    Id = 1, Role = "User", UserName = "GSK",
                    HumanInfo = new HumanInfo
                    {
                        PersonalId = 123, Name = "Alvydas", Surname = "Kazkas", PhoneNumber = "6845132",
                        Email = "testas@testas.lt",
                        Address = new Address
                            { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 }
                    }
                },
                new()
                {
                Id = 2, Role = "User", UserName = "KSG",
                HumanInfo = new HumanInfo
                {
                    PersonalId = 321, Name = "Petras", Surname = "Petraitis", PhoneNumber = "846512",
                    Email = "testas2@testas.lt",
                    Address = new Address
                        { City = "Kaunas", Street = "Larcenter", HouseNumber = 5, ApartmentNumber = 15 }
                }
                },
                new()
                {
                    Id = 3, Role = "User", UserName = "gskuodyte",
                    HumanInfo = new HumanInfo
                    {
                        PersonalId = 231, Name = "Ona", Surname = "Onauskiene", PhoneNumber = "87456123",
                        Email = "testas3@testas.lt",
                        Address = new Address
                            { City = "Klaipeda", Street = "Kobalto", HouseNumber = 9, ApartmentNumber = 62 }
                    }
                }
            };
        }

    }
}
