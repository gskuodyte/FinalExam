using HumanRegistrationSystem_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_DAL
{
    public class HumanRegistrationSystemDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Image> Images { get; set; }
        public HumanRegistrationSystemDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
