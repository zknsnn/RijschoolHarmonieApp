using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int StudentAccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
