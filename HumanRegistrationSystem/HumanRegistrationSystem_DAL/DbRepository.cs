using HumanRegistrationSystem_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.UserAccounts.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task InsertAccountAsync(UserAccount userAccount)
        {
            await _context.UserAccounts.AddAsync(userAccount);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);
        }

        public async Task<Image> GetImageAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
            
        }
    }
}
