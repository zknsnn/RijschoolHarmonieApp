using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.StudentAccount;

public class StudentAccountResponseDto
{
    public int StudentAccountId { get; set; }
    public int StudentId { get; set; }

    public string StudentName { get; set; }
    public string StudentAchternaam { get; set; }

    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public decimal Balance { get; set; }
}
