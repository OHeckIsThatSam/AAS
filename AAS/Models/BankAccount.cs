using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountID { get; set; }

        [Display(Name ="Account Number")]
        public string AccountNumber { get; set; }

        public double Balance { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
