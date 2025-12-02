namespace RijschoolHarmonieApp.DTOs.InstructorPrice
{
    // DTO returned in API responses
    public class InstructorPriceResponseDto
    {
        public int InstructorPriceId { get; set; }
        public int InstructorId { get; set; }
        public decimal LessonPrice { get; set; }
        public decimal ExamPrice { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
