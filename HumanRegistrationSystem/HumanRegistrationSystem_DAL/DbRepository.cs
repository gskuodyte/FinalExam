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
        }

        public Task SaveChangesAsync()
        {
             return _context.SaveChangesAsync();
        }

        public async Task<UserAccount> GetUserByIdAsync(int id)
        {
            return await _context.UserAccounts.Include(h => h.HumanInfo).Include(a => a.HumanInfo.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task DeleteUser(UserAccount userAccount)
        {
            
            _context.UserAccounts.Remove(userAccount);
            
            await SaveChangesAsync();
        }
    }

}
