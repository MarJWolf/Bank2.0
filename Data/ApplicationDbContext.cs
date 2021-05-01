using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Models;
using Bank.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Bank.Data
{
    public class ApplicationDbContext : IdentityDbContext<AccountUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");
            builder.Entity<AccountUser>();
            builder.Entity<IdentityRole>().ToTable("Position");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim <string>>().ToTable("UserClaim");
            builder.Entity<IdentityRoleClaim <string>>().ToTable("RoleClaim");
            //builder.Ignore<IdentityRole>();
            //builder.Ignore<IdentityUserClaim <string>>();
            //builder.Ignore<IdentityUserRole <string>>();
            //builder.Ignore<IdentityRoleClaim <string>>();
            builder.Ignore<IdentityUserLogin <string>>();
            builder.Ignore<IdentityUserToken <string>>();
        }
        public DbSet<AccountType> AccountType { get; set; }

        public DbSet<BankAccount> BankAccount { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Employee> Employee { get; set; }

        //public DbSet<Position> Position { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<TransactionCategory> TransactionCategory { get; set; }

    }
}
