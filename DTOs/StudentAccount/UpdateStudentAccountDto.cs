using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.StudentAccount
{
    public class UpdateStudentAccountDto
    {
        public int StudentAccountId { get; set; }
        public decimal? TotalDebit { get; set; }
        public decimal? TotalCredit { get; set; }
    }
}
