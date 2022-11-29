using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } 

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [ValidateNever]
        public string Permission { get; set; }

        [ValidateNever]
        public List<Log> Logs { get; set; }
    }
}
