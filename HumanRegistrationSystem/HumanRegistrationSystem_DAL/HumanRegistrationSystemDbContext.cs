using HumanRegistrationSystem_Domain;
using Microsoft.EntityFrameworkCore;

namespace HumanRegistrationSystem_DAL;

public class HumanRegistrationSystemDbContext : DbContext
{
    public HumanRegistrationSystemDbContext(DbContextOptions<HumanRegistrationSystemDbContext> options) : base(options)
    {
    }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<HumanInfo> Humans { get; set; }
    public DbSet<Address> Addresses { get; set; }
}