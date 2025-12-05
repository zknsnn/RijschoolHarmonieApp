using RijschoolHarmonieApp.Models;

public class AppointmentResponseDto
{
    public int AppointmentId { get; set; }
    public int InstructorId { get; set; }
    public int StudentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentType Type { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}