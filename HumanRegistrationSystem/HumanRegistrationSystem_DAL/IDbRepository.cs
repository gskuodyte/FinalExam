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
    }
}
