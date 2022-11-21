using System.ComponentModel.DataAnnotations;

namespace AAS.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public string Notes { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  
    }
}
