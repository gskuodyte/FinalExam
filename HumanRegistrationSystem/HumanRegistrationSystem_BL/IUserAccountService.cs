using DTO;
using HumanRegistrationSystem_Domain;
using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem_BL;

public interface IUserAccountService
{
    Task<bool> CreateUserAccountAsync(SignUpDto signUpDto, byte[] picture);
    Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username, string password);
    Task<UserAccountInfoResponce> GetMappedUserAccountAsync(int id);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> UpdateUserPersonalIdAsync(int id, string personalId);
    Task<bool> UpdateUserNameAsync(int id, string name);
    Task<bool> UpdateUserSurnameAsync(int id, string surname);
    Task<bool> UpdateUserPhoneNumberAsync(int id, string phoneNumber);
    Task<bool> UpdateUserEmailAsync(int id, string email);
    Task<bool> UpdateImageAsync(int id, byte[] picture);
    Task<bool> UpdateUserCityAddressAsync(int id, string city);
    Task<bool> UpdateUserStreetAddressAsync(int id, string street);
    Task<bool> UpdateUserHouseNumberAddressAsync(int id, int houseNumber);
    Task<bool> UpdateUserApartmentNumberAddressAsync(int id, int apartmentNumber);
    Task<byte[]> FileUploadAsync(IFormFile file, int width, int height);
}