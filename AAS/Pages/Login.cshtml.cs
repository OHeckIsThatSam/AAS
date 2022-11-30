using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AAS.Data;
using AAS.Models;

namespace AAS.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Context _context;

        public LoginModel(Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public string LoginType { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (LoginType == "default")
            {
                ModelState.AddModelError("LoginError", "You are not authorised to login.");
                return Page();
            }

            if (Employee == null) return Page();

            var employees = _context.Employees.Where(e => e.Username == Employee.Username);

            if (!employees.Any())
            {
                ModelState.AddModelError("LoginError", "Username or Password is incorrect.");
                return Page();
            }

            var employee = employees.First();

            if (employee.Password != Employee.Password)
            {
                ModelState.AddModelError("LoginError", "Username or Password is incorrect.");
                return Page();
            }

            HttpContext.Session.SetInt32("employeeId", employee.EmployeeId);
            HttpContext.Session.SetString("employeePermission", employee.Permission);

            return RedirectToPage("/Customers/Index");
        }
    }
}
