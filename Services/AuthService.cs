using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.DTOs.Auth;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Security;

namespace RijschoolHarmonieApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly RijschoolHarmonieAppContext _context;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthService(RijschoolHarmonieAppContext context, JwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return null;

            // ✔ Check Hash
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = _tokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Role = user.Role.ToString(),
                UserId = user.UserId,
            };
        }
    }
}
