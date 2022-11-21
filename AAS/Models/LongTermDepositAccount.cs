using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AAS.Models
{
    public class LongTermDepositAccount : BankAccount
    {
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = false;
    }
}
