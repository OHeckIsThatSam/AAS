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
    public class LongTermDepositAccountDetailsModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public LongTermDepositAccountDetailsModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public LongTermDepositAccount LongTermDepositAccount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Activate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (id == null || _context.LongTermDepositAccounts == null)
            {
                return NotFound();
            }

            var longtermdepositaccount = await _context.LongTermDepositAccounts.FirstOrDefaultAsync(m => m.BankAccountID == id);
            if (longtermdepositaccount == null)
            {
                return NotFound();
            }
            else 
            {
                LongTermDepositAccount = longtermdepositaccount;

                if (Activate == "activate")
                {
                    LongTermDepositAccount.IsActive = true;
                    _context.LongTermDepositAccounts.Update(LongTermDepositAccount);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
