using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AAS.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(11)]
        public string Telephone { get; set; }

        [ValidateNever]
        [Display(Name = "Name")]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [ValidateNever]
        public List<Log> Logs { get; set; }
    }
}
