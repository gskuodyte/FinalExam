using HumanRegistrationSystem_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_DAL
{
    public interface IDbRepository
    {
        Task<UserAccount?> GetAccountByUserNameAsync(string username);
        Task InsertAccountAsync(UserAccount userAccount);
        Task AddImageAsync(Image image);
        Task<Image> GetImageAsync(int id);
        Task SaveChangesAsync();
    }
}
