using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;
using System.Diagnostics;

namespace AAS.Pages.Accounts
{
    public class CurrentAccountDetailsModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public CurrentAccountDetailsModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public CurrentAccount CurrentAccount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Overdraft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (id == null || _context.CurrentAccounts == null)
            {
                return NotFound();
            }

            var currentaccount = await _context.CurrentAccounts.FirstOrDefaultAsync(m => m.BankAccountID == id);
            if (currentaccount == null)
            {
                return NotFound();
            }
            else 
            {
                CurrentAccount = currentaccount;

                if (Overdraft != 0)
                {
                    CurrentAccount.Overdraft = Overdraft;
                    _context.CurrentAccounts.Update(CurrentAccount);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
