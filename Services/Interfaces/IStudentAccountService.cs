using RijschoolHarmonieApp.DTOs;
using RijschoolHarmonieApp.DTOs.StudentAccount;

namespace RijschoolHarmonieApp.Services
{
    public interface IStudentAccountService
    {
        Task<List<StudentAccountResponseDto>> GetAllAsync();
        Task<StudentAccountResponseDto?> GetByIdAsync(int id);
        Task<StudentAccountResponseDto> AddAsync(CreateStudentAccountDto dto);
        Task<StudentAccountResponseDto?> UpdateAsync(UpdateStudentAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}