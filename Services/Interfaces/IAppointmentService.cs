using RijschoolHarmonieApp.DTOs.Appointment;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Services
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDto>> GetAllAsync();
        Task<AppointmentResponseDto?> GetByIdAsync(int id);

        Task<List<AppointmentResponseDto>> GetByInstructorAsync(
            int instructorId,
            DateTime? start = null,
            DateTime? end = null
        );

        Task<List<AppointmentResponseDto>> GetByStudentAsync(
            int studentId,
            DateTime? start = null,
            DateTime? end = null
        );

        Task<List<AppointmentResponseDto>> GetFilteredAsync(
            int? instructorId,
            int? studentId,
            DateTime? start,
            DateTime? end,
            AppointmentType? type
        );

        Task<AppointmentResponseDto> CreateAsync(CreateAppointmentDto dto);
        Task<AppointmentResponseDto?> UpdateAsync(UpdateAppointmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
