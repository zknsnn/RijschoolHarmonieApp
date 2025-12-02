using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.InstructorPrice
{
    public class CreateInstructorPriceDto
    {
        public int InstructorId { get; set; } // FK to User (Instructor)
        public decimal LessonPrice { get; set; } // Price per lesson
        public decimal ExamPrice { get; set; }
    }
}
