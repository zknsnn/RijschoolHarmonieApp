using System.ComponentModel.DataAnnotations.Schema;

namespace RijschoolHarmonieApp.Models
{
    public class StudentAccount
    {
        public int StudentAccountId { get; set; } // PK

        public int StudentId { get; set; } // FK
        public User Student { get; set; } // Navigation property

        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Balance { get; private set; } // Calculated in DB

        public List<Payment> Payments { get; set; }

        public StudentAccount()
        {
            Payments = new List<Payment>();
        }
    }
}
