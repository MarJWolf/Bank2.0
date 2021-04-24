using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Models;

namespace Bank.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccountType> AccountType { get; set; }

        public DbSet<BankAccount> BankAccount { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Position> Position { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<TransactionCategory> TransactionCategory { get; set; }
    }
}
