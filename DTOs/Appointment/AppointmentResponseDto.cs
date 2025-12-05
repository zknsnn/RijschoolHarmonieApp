using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.Appointment
{
    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public int InstructorId { get; set; }
        public string InstructorName { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public AppointmentType Type { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }
    }
}
