using HumanRegistrationSystem_Domain;

namespace HumanRegistrationSystem_DAL
{
    public interface IDbRepository
    {
        Task<UserAccount?> GetAccountByUserNameAsync(string username);
        Task InsertAccountAsync(UserAccount userAccount);
        Task<UserAccount?> GetUserByIdAsync(int id);
        Task SaveChangesAsync();
        Task DeleteUser(UserAccount userAccount);
        Task<bool> UpdatePersonalId(int id, int personalId);
        Task<bool> UpdateName(int id, string name);
        Task<bool> UpdateSurname(int id, string surname);
        Task<bool> UpdatePhoneNumber(int id, string phoneNumber);
        Task<bool> UpdateEmail(int id, string email);
        Task<bool> UpdateImage(int id, byte[] picture);
        Task<bool> UpdateCity(int id, string city);
        Task<bool> UpdateStreet(int id, string street);
        Task<bool> UpdateHouseNumber(int id, int houseNumber);
        Task<bool> UpdateApartmentNumber(int id, int apartmentNumber);
    }
}
