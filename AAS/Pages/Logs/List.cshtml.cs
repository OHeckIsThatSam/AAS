using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages.Logs
{
    public class ListModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public ListModel(AAS.Data.Context context)
        {
            _context = context;
        }

        public IList<Log> Log { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (HttpContext.Session.GetString("employeeId") == null) Response.Redirect("../Login");

            if (_context.Logs != null)
            {
                Log = await _context.Logs
                .Include(l => l.Customer)
                .Include(l => l.Employee).ToListAsync();
            }
        }
    }
}
