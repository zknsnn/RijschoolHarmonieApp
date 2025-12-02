using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.StudentAccount
{
    public class CreateStudentAccountDto
    {
        public int StudentId { get; set; } // FK
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
