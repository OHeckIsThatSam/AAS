using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Cards
{
    public class DetailsModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public DetailsModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public Card Card { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Activate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }
            else 
            {
                Card = card;

                if (Activate == "activate")
                {
                    Card.IsActive = true;
                    Card.PIN = RandomNumbers(4);
                    _context.Cards.Update(Card);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
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
