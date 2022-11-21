using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Accounts
{
    public class DepositAccountDetailsModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public DepositAccountDetailsModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public DepositAccount DepositAccount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Activate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (id == null || _context.DepositAccounts == null)
            {
                return NotFound();
            }

            var depositaccount = await _context.DepositAccounts.FirstOrDefaultAsync(m => m.BankAccountID == id);
            if (depositaccount == null)
            {
                return NotFound();
            }
            else 
            {
                DepositAccount = depositaccount;

                if (Activate == "activate")
                {
                    DepositAccount.IsActive = true;
                    _context.DepositAccounts.Update(DepositAccount);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
