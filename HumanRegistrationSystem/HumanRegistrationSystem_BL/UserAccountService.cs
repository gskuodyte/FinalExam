using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using System.Security.Cryptography;
using System.Text;

namespace HumanRegistrationSystem_BL
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IDbRepository _dbRepository;

        public UserAccountService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public async Task<bool> CreateUserAccountAsync(string userName, string password, HumanDto human, AddressDto address, Image image)
        {
            var existingUser = await _dbRepository.GetAccountByUserNameAsync(userName);
            if (existingUser != null)
            {
                return false;
            }

            var (hash, salt) = CreatePasswordHash(password);

            var newUser = new UserAccount
            {
                UserName = userName,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "User",
                Human = new Human
                {
                    Name = human.Name,
                    Surname = human.Surname,
                    PersonalID = human.PersonalID,
                    TelephoneNumber = human.TelephoneNumber,
                    Email = human.Email,
                    Image = image,

                    Address = new Address
                    {
                        City = address.City,
                        Street = address.Street,
                        HouseNumber = address.HouseNumber,
                        ApartmentNumber = address.ApartmentNumber
                    }
                }
            };

            await _dbRepository.InsertAccountAsync(newUser);
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username, string password)
        {
            var account = await _dbRepository.GetAccountByUserNameAsync(username);
            if (account == null)
            {
                return (false, null);
            }

            if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return (true, account);
            }
            else
            {
                return (false, null);
            }
        }
        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (hash, salt);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
