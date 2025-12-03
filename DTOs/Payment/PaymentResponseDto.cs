public class PaymentResponseDto
{
    public int PaymentId { get; set; }

    public int StudentAccountId { get; set; }
    public string StudentName { get; set; } = null!; 
    public string StudentLastName { get; set; } = null!; 

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public string? Description { get; set; }
}
