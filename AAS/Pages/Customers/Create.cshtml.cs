using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public CreateModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            //Create the card aswell
            Card card = new();
            card.Number = RandomNumbers(11);
            card.SecurityCode = RandomNumbers(3);
            card.ExpiryDate = DateTime.Today.AddYears(3);
            card.PIN = RandomNumbers(4);
            card.CustomerId = Customer.CustomerId;

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            CurrentAccount currentAccount = new();
            DepositAccount depositAccount = new();
            LongTermDepositAccount longTermDepositAccount = new();

            currentAccount.AccountNumber = RandomNumbers(8);
            depositAccount.AccountNumber = RandomNumbers(8);
            longTermDepositAccount.AccountNumber = RandomNumbers(8);

            currentAccount.Balance = depositAccount.Balance = longTermDepositAccount.Balance = 0;

            currentAccount.CustomerId = depositAccount.CustomerId = longTermDepositAccount.CustomerId = Customer.CustomerId; ;
            
            currentAccount.Overdraft = 0;
            currentAccount.Salary = 0;

            _context.CurrentAccounts.Add(currentAccount);
            _context.DepositAccounts.Add(depositAccount);
            _context.LongTermDepositAccounts.Add(longTermDepositAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string RandomNumbers(int n)
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
