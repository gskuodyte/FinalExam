
using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_Domain;

namespace HumanRegistrationSystem_BL
{
    public interface IUserAccountService
    {
        Task<bool> CreateUserAccountAsync(string userName, string password, HumanDto human, AddressDto address, Image image);
        Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username, string password);
    }
}
