using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AAS.Models;

namespace AAS.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<AAS.Models.Customer> Customers { get; set; } = default!;
        public DbSet<AAS.Models.Card> Cards { get; set; } = default!;
        public DbSet<AAS.Models.CurrentAccount> CurrentAccounts { get; set; } = default!;
        public DbSet<AAS.Models.DepositAccount> DepositAccounts { get; set; } = default!;
        public DbSet<AAS.Models.LongTermDepositAccount> LongTermDepositAccounts { get; set; } = default!;
        public DbSet<AAS.Models.Employee> Employees { get; set; } = default!;
        public DbSet<AAS.Models.Log> Logs { get; set; } = default!;
        public DbSet<AAS.Models.Transaction> Transactions { get; set; } = default!;
        public DbSet<AAS.Models.BankAccount> BankAccount { get; set; }
    }
}
