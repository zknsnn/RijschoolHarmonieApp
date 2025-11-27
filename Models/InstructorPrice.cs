namespace RijschoolHarmonieApp.Models
{
    public class InstructorPrice
    {
        public int InstructorPriceId { get; set; }

        public int InstructorId { get; set; }
        public User Instructor { get; set; }

        public decimal LessonPrice { get; set; }

        public decimal ExamPrice { get; set; }

        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public InstructorPrice(int instructorId, decimal lessonPrice, decimal examPrice)
        {
            InstructorId = instructorId;
            LessonPrice = lessonPrice;
            ExamPrice = examPrice;
            LastUpdateDate = DateTime.Now;
        }

        private InstructorPrice() { }
    }
}
