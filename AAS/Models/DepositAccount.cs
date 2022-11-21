using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class DepositAccount : BankAccount
    {
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = false;
    }
}
