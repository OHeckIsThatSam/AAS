using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [StringLength(19)]
        [Display(Name = "Cards Number")]
        public string Number { get; set; }

        [StringLength(3)]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(4)]
        public string PIN { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [ValidateNever]
        public int CustomerId { get; set; }

        [ValidateNever]
        public Customer Customer { get; set; }
    }
}
