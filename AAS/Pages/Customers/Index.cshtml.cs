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
    public class IndexModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public IndexModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (HttpContext.Session.GetString("employeeId") == null) Response.Redirect("./Login");

            var bankAccounts = from a in _context.BankAccount
                               select a;

            if (!string.IsNullOrEmpty(SearchString))
            {
                var account = bankAccounts.Where(a => a.AccountNumber.Contains(SearchString)).FirstOrDefault();

                if (account != null)
                {
                    var customer = _context.Customers.Where(c => c.CustomerId == account.CustomerId);

                    Customer = customer.ToList();
                }
            }

            if (_context.Customers != null && Customer == null)
            {
                Customer = await _context.Customers.ToListAsync();
            }
        }
    }
}
