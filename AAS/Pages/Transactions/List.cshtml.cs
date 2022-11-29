using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;
using NuGet.Packaging;

namespace AAS.Pages.Transactions
{
    public class ListModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public ListModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        public async Task OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) Response.Redirect("./Login");

            if (_context.Transactions != null && id != null)
            {
                Customer customer = _context.Customers.Where(
                    c => c.CustomerId == id).First();

                if (customer == null) Response.Redirect("../Customers");

                var bankAccounts = _context.BankAccount.Where(
                    c => c.CustomerId == id).ToList();

                Transaction = new List<Transaction>();

                foreach (var bankAccount in bankAccounts)
                {
                    Transaction.AddRange(_context.Transactions.Where(
                        t => t.BankAccountID == bankAccount.BankAccountID));
                }
                Transaction = Transaction.OrderByDescending(
                    t => t.TransactionDate).ToList();

                Transaction = Transaction.Skip(
                    Math.Max(0, Transaction.Count - 10)).ToList();
            }
            else
            {
                Transaction = await _context.Transactions
                .Include(t => t.BankAccount).ToListAsync();
            }
        }
    }
}
