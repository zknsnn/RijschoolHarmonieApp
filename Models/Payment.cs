namespace RijschoolHarmonieApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int StudentAccountId { get; set; }
        public StudentAccount StudentAccount { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }
    }
}
