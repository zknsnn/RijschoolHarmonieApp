using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int PaymentId { get; set; }

        public int StudentId { get; set; }
        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}
