using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Cards
{
    public class EditModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public EditModel(AAS.Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Card Card { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");
            if (HttpContext.Session.GetString("employeePermission") == "vpn") return RedirectToPage("../Customers/Index");

            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card =  await _context.Cards.FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }
            Card = card;
           ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            return Page();
        }
    }
}
