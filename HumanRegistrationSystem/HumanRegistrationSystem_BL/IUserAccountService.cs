
using System.Drawing;
using DTO;
using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem_BL
{
    public interface IUserAccountService
    {
        Task<bool> CreateUserAccountAsync(SignUpDto signUpDto, byte[] Picture);
        Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username, string password);
        Task<UserAccount> GetUserById(int id);
        Task<UserAccountInfoResponce> GetMapedUserAccount(int id);
        Task DeleteUser(UserAccount userAccount);
        Task<byte[]> ResizeImage(Image image, int width, int height);
        Task<bool> UpdateUserPersonalId(int id, int personalId);
        Task<bool> UpdateUserName(int id, string name);
        Task<bool> UpdateUserSurname(int id, string surname);
        Task<bool> UpdateUserPhoneNumber(int id, string phoneNumber);
        Task<bool> UpdateUserEmail(int id, string email);
        Task<bool> UpdateImageAsync(int id, byte[]  picture);
        Task<bool> UpdateUserCityAddress(int id, string city);
        Task<bool> UpdateUserStreetAddress(int id, string street);
        Task<bool> UpdateUserHouseNumberAddress(int id, int houseNumber);
        Task<bool> UpdateUserApartmentNumberAddress(int id, int apartmentNumber);
        Task<byte[]> FileUpload(IFormFile file, int width, int height);
    }
}
