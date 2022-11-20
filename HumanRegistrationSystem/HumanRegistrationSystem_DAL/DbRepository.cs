using System.Data;
using System.Diagnostics;
using HumanRegistrationSystem_Domain;
using Microsoft.EntityFrameworkCore;

namespace HumanRegistrationSystem_DAL
{
    public class DbRepository : IDbRepository
    {
        private readonly HumanRegistrationSystemDbContext _context;

        public DbRepository(HumanRegistrationSystemDbContext context)
        {
            _context = context;
        }
        public async Task<UserAccount?> GetAccountByUserNameAsync(string username)
        {
            return await _context.UserAccounts.Include(h => h.HumanInfo).Include(a => a.HumanInfo.Address).SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task InsertAccountAsync(UserAccount userAccount)
        {
            await _context.UserAccounts.AddAsync(userAccount);
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<UserAccount?> GetUserByIdAsync(int id)
        {
            return (await _context.UserAccounts.Include(h => h.HumanInfo).Include(a => a.HumanInfo.Address).FirstOrDefaultAsync(i => i.Id == id))!;
        }

        public async Task DeleteUser(UserAccount userAccount)
        {

            _context.UserAccounts.Remove(userAccount);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePersonalId(int id, int personalId)
        {

            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.HumanInfo.PersonalId = personalId;
                await SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.HumanInfo.Name = name;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSurname(int id, string surname)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Surname = surname;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePhoneNumber(int id, string phoneNumber)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.PhoneNumber = phoneNumber;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateEmail(int id, string email)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Email = email;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateImage(int id, byte[] picture)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Picture = picture;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCity(int id, string city)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Address.City = city;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateStreet(int id, string street)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Address.Street = street;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateHouseNumber(int id, int houseNumber)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Address.HouseNumber = houseNumber;
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateApartmentNumber(int id, int apartmentNumber)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user!.HumanInfo.Address.ApartmentNumber = apartmentNumber;
                await SaveChangesAsync();
                return true;
            }
            return false;

        }
    }

}
