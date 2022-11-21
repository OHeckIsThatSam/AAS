using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public double Amount { get; set; }

        [Display(Name = "Date")]
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; }

        public int BankAccountID { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
