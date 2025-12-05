using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public int InstructorId { get; set; }
        public int StudentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentType Type { get; set; }
    }
}
