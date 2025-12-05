using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.InstructorPrice
{
    public class UpdateAppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public AppointmentType? Type { get; set; }
    }
}
