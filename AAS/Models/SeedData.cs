using AAS.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AAS.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AAS.Data.Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AAS.Data.Context>>()))
            {
                if (context == null || context.Customers == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any customers.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

                List<Customer> customers = new();

                customers.Add(new Customer
                {
                    FirstName = "Sam",
                    LastName = "Robbins",
                    Email = "sam@example.net",
                    Address = "Cantor College, Arundel Street",
                    Telephone = "00000000000"
                });
                customers.Add(new Customer
                {
                    FirstName = "Syafiq",
                    LastName = "Zolkeply",
                    Email = "syafiq@example.net",
                    Address = "Cantor College, Arundel Street",
                    Telephone = "11111111111"
                });
                customers.Add(new Customer
                {
                    FirstName = "Mehmet",
                    LastName = "Özcan",
                    Email = "mehmet@example.net",
                    Address = "Cantor College, Arundel Street",
                    Telephone = "22222222222"
                });
                customers.Add(new Customer
                {
                    FirstName = "Imraan",
                    LastName = "Khan",
                    Email = "imraan@example.net",
                    Address = "Cantor College, Arundel Street",
                    Telephone = "33333333333"
                });

                context.Customers.AddRange(customers);

                context.SaveChanges();

                foreach (Customer c in customers)
                {
                    context.Cards.Add (new Card
                    {
                        Number = RandomNumbers(11),
                        SecurityCode = RandomNumbers(3),
                        ExpiryDate = DateTime.Today.AddYears(3),
                        PIN = RandomNumbers(4),
                        CustomerId = c.CustomerId
                    });

                    CurrentAccount currentAccount = new();
                    DepositAccount depositAccount = new();
                    LongTermDepositAccount longTermDepositAccount = new();

                    currentAccount.AccountNumber = RandomNumbers(8);
                    depositAccount.AccountNumber = RandomNumbers(8);
                    longTermDepositAccount.AccountNumber = RandomNumbers(8);

                    currentAccount.Balance = depositAccount.Balance = longTermDepositAccount.Balance = 100;

                    currentAccount.CustomerId = depositAccount.CustomerId = longTermDepositAccount.CustomerId = c.CustomerId;

                    currentAccount.Overdraft = 0;
                    currentAccount.Salary = 0;

                    context.CurrentAccounts.Add(currentAccount);
                    context.DepositAccounts.Add(depositAccount);
                    context.LongTermDepositAccounts.Add(longTermDepositAccount);
                }
                context.SaveChanges();

                context.Employees.Add(
                    new Employee
                    {
                        Username = "admin",
                        Password = "admin",
                        Permission = "admin"
                    });
                context.Employees.Add(
                    new Employee
                    {
                        Username = "branch",
                        Password = "branch",
                        Permission = "branch"
                    });
                context.Employees.Add(
                    new Employee
                    {
                        Username = "vpn",
                        Password = "vpn",
                        Permission = "vpn"
                    });
                context.SaveChanges();
            }
        }

        private static string RandomNumbers(int n)
        {
            string number = "";
            Random random = new();

            for (int i = 0; i < n; i++)
            {
                number += random.Next(10).ToString();
            }
            return number;
        }
    }
}
