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
        var sut = GetExistingUser();
        var image = Array.Empty<byte>();
        var data = await sut.CreateUserAccountAsync(CreateSignUpUser(), image);
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
    public async Task UpdateUserPersonalId_WhenUserExists()
    {
        var sut = GetExistingUser();
        var data = await sut.UpdateUserPersonalIdAsync(1, "6846321");
        Assert.True(data);
    }

    [Fact]
    public async Task UpdateUserPersonalId_WhenUserDoesNotExists()
    {
        var sut = GetNewUser();
        var data = await sut.UpdateUserPersonalIdAsync(2, "58474869");
        Assert.False(data);
    }

    [Fact]
    public async Task UpdateUserName_WhenUserExists()
    {
        var sut = GetExistingUser();
        var data = await sut.UpdateUserNameAsync(1, "Ona");
        Assert.True(data);
    }

    [Fact]
    public async Task GetMappedUserAccount_WhenUserExists()
    {
        var sut = GetExistingUser();
        var data = await sut.GetMappedUserAccountAsync(1);
        Assert.Equal("Alvydas", data.Name);
    }
   
    [Fact]
    public async Task UpdateUserName_WhenUserDoNotExists()
    {
        var sut = GetNewUser();
        var data = await sut.UpdateUserNameAsync(2, "Andrius");
        Assert.False(data);
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