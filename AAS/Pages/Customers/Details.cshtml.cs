using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public DetailsModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }
        public Card Card { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        public DepositAccount DepositAccount { get; set; }
        public LongTermDepositAccount LongTermDepositAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            var card = await _context.Cards.FirstOrDefaultAsync(m => m.CustomerId == id);
            var currentAccount = await _context.CurrentAccounts.FirstOrDefaultAsync(m => m.CustomerId == id);
            var depositAccount = await _context.DepositAccounts.FirstOrDefaultAsync(m => m.CustomerId == id);
            var longTermDepositAccount = await _context.LongTermDepositAccounts.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
                Card = card;
                CurrentAccount = currentAccount;
                DepositAccount = depositAccount;
                LongTermDepositAccount = longTermDepositAccount;
            }
            return Page();
        }
    }
}
