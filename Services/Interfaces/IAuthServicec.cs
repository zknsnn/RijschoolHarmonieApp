using RijschoolHarmonieApp.DTOs.Auth;

namespace RijschoolHarmonieApp.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    }
}
