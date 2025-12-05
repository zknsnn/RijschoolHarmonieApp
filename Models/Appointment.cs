namespace RijschoolHarmonieApp.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // PK

        public int InstructorId { get; set; }
        public User? Instructor { get; set; }

        public int StudentId { get; set; }
        public User? Student { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public AppointmentType Type { get; set; }

        public decimal Price { get; set; } // Calculated automatically

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
