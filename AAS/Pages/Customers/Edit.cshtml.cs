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
using System.Security.Principal;
using System.Text;

namespace AAS.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly AAS.Data.Context _context;

        public EditModel(AAS.Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        private Customer orignalCustomer;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("employeeId") == null) return RedirectToPage("../Login");
            if (HttpContext.Session.GetString("employeePermission") == "vpn") return RedirectToPage("../Customers/Index");

            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer =  await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            orignalCustomer = Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetInt32("employeeId") == null) return RedirectToPage("../Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == Customer.CustomerId);

            Log log = new();
            log.CustomerId = Customer.CustomerId;
            log.EmployeeId = (int)HttpContext.Session.GetInt32("employeeId");
            log.LogDate = DateTime.Now;

            StringBuilder sb = new();
            sb.Append("Changes: ");

            var originalValues = _context.Entry(customer).OriginalValues;
            var currentValues = _context.Entry(Customer).CurrentValues;

            foreach (var propertyName in originalValues.Properties)
            {
                var original = originalValues[propertyName.Name];
                var current = currentValues[propertyName.Name];

                if (!Equals(original, current))
                {
                    sb.Append($"{propertyName.Name} : {original} ");
                    sb.Append($"---> {current} ");
                }
            }

            log.Notes = sb.ToString();

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            _context.Entry(customer).State = EntityState.Detached;
            _context.Entry(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
          return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
