using RijschoolHarmonieApp.DTOs.User;

namespace RijschoolHarmonieApp.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<UserResponseDto> AddAsync(CreateUserDto dto);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
    }
}