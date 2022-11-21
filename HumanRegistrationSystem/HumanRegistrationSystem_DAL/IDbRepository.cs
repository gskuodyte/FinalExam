using HumanRegistrationSystem_Domain;

namespace HumanRegistrationSystem_DAL;

public interface IDbRepository
{
    Task<UserAccount?> GetAccountByUserNameAsync(string username);
    Task InsertAccountAsync(UserAccount userAccount);
    Task<UserAccount?> GetUserByIdAsync(int id);
    Task DeleteUserAsync(UserAccount userAccount);
    Task<bool> UpdatePersonalIdAsync(int id, string personalId);
    Task<bool> UpdateNameAsync(int id, string name);
    Task<bool> UpdateSurnameAsync(int id, string surname);
    Task<bool> UpdatePhoneNumberAsync(int id, string phoneNumber);
    Task<bool> UpdateEmailAsync(int id, string email);
    Task<bool> UpdateImageAsync(int id, byte[] picture);
    Task<bool> UpdateCityAsync(int id, string city);
    Task<bool> UpdateStreetAsync(int id, string street);
    Task<bool> UpdateHouseNumberAsync(int id, int houseNumber);
    Task<bool> UpdateApartmentNumberAsync(int id, int apartmentNumber);
}