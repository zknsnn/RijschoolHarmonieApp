using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int appointmentId);

        Task<List<Appointment>> GetByInstructorAsync(
            int instructorId,
            DateTime? start = null,
            DateTime? end = null
        );
        Task<List<Appointment>> GetByStudentAsync(
            int studentId,
            DateTime? start = null,
            DateTime? end = null
        );
        Task<List<Appointment>> GetFilteredAsync(
            int? instructorId,
            int? studentId,
            DateTime? start,
            DateTime? end,
            AppointmentType? type
        );
        Task<Appointment> AddAsync(Appointment appointment);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(int id);
    }
}
