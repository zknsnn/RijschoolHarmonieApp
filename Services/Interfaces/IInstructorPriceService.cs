using RijschoolHarmonieApp.DTOs.InstructorPrice;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Services
{
    public interface IInstructorPriceService
    {
        Task<List<InstructorPriceResponseDto>> GetAllAsync();
        Task<InstructorPriceResponseDto?> GetByIdAsync(int id);
        Task<InstructorPriceResponseDto> AddAsync(CreateInstructorPriceDto dto);
        Task<InstructorPriceResponseDto?> UpdateAsync(UpdateInstructorPriceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
