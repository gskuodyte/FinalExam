using DTO;
using HumanRegistrationSystem_BL;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using Moq;

namespace BusinessLogic_UniTests;

public class UserAccountServiceTests
{
    [Fact]
    public async Task TrySignUp_WhenUserAlreadyExists_()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.GetAccountByUserNameAsync("GSK")).ReturnsAsync(CreateExistingUser());
        dbRepositoryMock.Setup(x => x.GetUserByIdAsync(1)).ReturnsAsync(CreateExistingUser());

        var sut = new UserAccountService(dbRepositoryMock.Object);
        
        var image = Array.Empty<byte>();
        var data = await sut.CreateUserAccountAsync(CreateSignUpUser(), image);

        dbRepositoryMock.Verify(x => x.GetAccountByUserNameAsync("GSK"), Times.Once);
        Assert.False(data);
    }

   

    [Fact]
    public async Task UpdateUserPersonalId_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdatePersonalIdAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserPersonalIdAsync(1, "6846321");

        dbRepositoryMock.Verify(u => u.UpdatePersonalIdAsync(1, "6846321"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserPersonalId_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdatePersonalIdAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserPersonalIdAsync(2, "6846321");

        dbRepositoryMock.Verify(u => u.UpdatePersonalIdAsync(2, "6846321"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserName_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateNameAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserNameAsync(1, "Ona");

        dbRepositoryMock.Verify(u => u.UpdateNameAsync(1, "Ona"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserName_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateNameAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserNameAsync(2, "Ona");

        dbRepositoryMock.Verify(u => u.UpdateNameAsync(2, "Ona"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserSurname_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateSurnameAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserSurnameAsync(1, "Onaityte");

        dbRepositoryMock.Verify(u => u.UpdateSurnameAsync(1, "Onaityte"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserSurname_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateSurnameAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserSurnameAsync(2, "Onaityte");

        dbRepositoryMock.Verify(u => u.UpdateSurnameAsync(2, "Onaityte"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserEmail_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateEmailAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserEmailAsync(1, "test@test.lt");

        dbRepositoryMock.Verify(u => u.UpdateEmailAsync(1, "test@test.lt"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserEmail_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateEmailAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserEmailAsync(2, "test@test.lt");

        dbRepositoryMock.Verify(u => u.UpdateEmailAsync(2, "test@test.lt"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserPhone_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdatePhoneNumberAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserPhoneNumberAsync(1, "+370693");

        dbRepositoryMock.Verify(u => u.UpdatePhoneNumberAsync(1, "+370693"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserPhone_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdatePhoneNumberAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserPhoneNumberAsync(2, "+370693");

        dbRepositoryMock.Verify(u => u.UpdatePhoneNumberAsync(2, "+370693"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserImage_WhenUserExists()
    {
        var imageBytes = new byte[100];
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateImageAsync(1, It.IsAny<byte[]>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateImageAsync(1, imageBytes);

        dbRepositoryMock.Verify(u => u.UpdateImageAsync(1, imageBytes), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserImage_WhenUserDoesNotExists()
    {
        var imageBytes = new byte[100];
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateImageAsync(1, It.IsAny<byte[]>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateImageAsync(2, imageBytes);

        dbRepositoryMock.Verify(u => u.UpdateImageAsync(2, imageBytes), Times.Once);
        Assert.False(data);
    }


    [Fact]
    public async Task UpdateUserCity_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateCityAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserCityAddressAsync(1, "Vilnius");

        dbRepositoryMock.Verify(u => u.UpdateCityAsync(1, "Vilnius"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserCity_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateCityAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserCityAddressAsync(2, "Vilnius");

        dbRepositoryMock.Verify(u => u.UpdateCityAsync(2, "Vilnius"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserStreet_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateStreetAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserStreetAddressAsync(1, "Vilnius");

        dbRepositoryMock.Verify(u => u.UpdateStreetAsync(1, "Vilnius"), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserStreet_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateStreetAsync(1, It.IsAny<string>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserStreetAddressAsync(2, "Vilnius");

        dbRepositoryMock.Verify(u => u.UpdateStreetAsync(2, "Vilnius"), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserHouseNum_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateHouseNumberAsync(1, It.IsAny<int>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserHouseNumberAddressAsync(1, 10);

        dbRepositoryMock.Verify(u => u.UpdateHouseNumberAsync(1, 10), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserHouseNum_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateHouseNumberAsync(1, It.IsAny<int>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserHouseNumberAddressAsync(2, 10);

        dbRepositoryMock.Verify(u => u.UpdateHouseNumberAsync(2, 10), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserApartmentNum_WhenUserExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateApartmentNumberAsync(1, It.IsAny<int>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserApartmentNumberAddressAsync(1, 10);

        dbRepositoryMock.Verify(u => u.UpdateApartmentNumberAsync(1, 10), Times.Once);
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserApartmentNum_WhenUserDoesNotExists()
    {
        var dbRepositoryMock = new Mock<IDbRepository>();
        dbRepositoryMock.Setup(x => x.UpdateApartmentNumberAsync(1, It.IsAny<int>())).ReturnsAsync(true);

        var sut = new UserAccountService(dbRepositoryMock.Object);
        var data = await sut.UpdateUserApartmentNumberAddressAsync(2, 10);

        dbRepositoryMock.Verify(u => u.UpdateApartmentNumberAsync(2, 10), Times.Once);
        Assert.False(data);
    }

    [Fact]
    public async Task TrySignUp_WithNewUser()
    {
        var sut = GetNewUser();
        var image = Array.Empty<byte>();
        var data = await sut.CreateUserAccountAsync(CreateSignUpUser(), image);

        Assert.True(data);
    }

    [Fact]
    public async Task TryLogin_WithNewUser()
    {
        var sut = GetNewUser();
        var data = await sut.LoginAsync(CreateSignUpUser().UserName, CreateSignUpUser().Password);
        Assert.False(data.authenticationSuccessful);
    }

    [Fact]
    public async Task GetMappedUserAccount_WhenUserExists()
    {
        var sut = GetExistingUser();
        var data = await sut.GetMappedUserAccountAsync(1);
        Assert.Equal("Alvydas", data.Name);
    }

    [Fact]
    public async Task DeleteUser_WhenUserExists()
    {
        var sut = GetExistingUser();
        var data = await sut.DeleteUserAsync(1);
        Assert.True(data);
    }

    [Fact]
    public IUserAccountService GetExistingUser()
    {
        var userAccountItems = new Mock<IDbRepository>();
        userAccountItems.Setup(x => x.GetAccountByUserNameAsync("GSK")).ReturnsAsync(CreateExistingUser());
        userAccountItems.Setup(x => x.GetUserByIdAsync(1)).ReturnsAsync(CreateExistingUser());
        userAccountItems.Setup(x => x.UpdatePersonalIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateSurnameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdatePhoneNumberAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateEmailAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateImageAsync(It.IsAny<int>(), It.IsAny<byte[]>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateCityAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateStreetAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateHouseNumberAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateApartmentNumberAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
        var sut = new UserAccountService(userAccountItems.Object);

        return sut;
    }

    public IUserAccountService GetNewUser()
    {
        var userAccountItems = new Mock<IDbRepository>();
        userAccountItems.Setup(x => x.GetAccountByUserNameAsync("gskuodyte")).ReturnsAsync(CreateNewUser());
        userAccountItems.Setup(x => x.GetUserByIdAsync(1)).ReturnsAsync(CreateNewUser());
        userAccountItems.Setup(x => x.UpdatePersonalIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);
        userAccountItems.Setup(x => x.UpdateNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);
        userAccountItems.Setup(x => x.UpdateSurnameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdatePhoneNumberAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateEmailAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateImageAsync(It.IsAny<int>(), It.IsAny<byte[]>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateCityAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateStreetAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateHouseNumberAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
        userAccountItems.Setup(x => x.UpdateApartmentNumberAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
        var sut = new UserAccountService(userAccountItems.Object);
        return sut;
    }

    public UserAccount CreateExistingUser()
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

    public UserAccount CreateNewUser()
    {
        var user = new UserAccount
        {
            Id = 1,
            Role = "User",
            UserName = "gskuodyte",
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

    public SignUpDto CreateSignUpUser()
    {
        var user = new SignUpDto
        {
            Password = "testas",
            UserName = "GSK",
            PersonalId = "123",
            Name = "Alvydas",
            Surname = "Kazkas",
            PhoneNumber = "6845132",
            Email = "testas@testas.lt",
            Address = new AddressDto { City = "Vilnius", Street = "Kugsgatan", HouseNumber = 2, ApartmentNumber = 10 }
        };
        return user;
    }
}