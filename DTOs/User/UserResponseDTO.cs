using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public int? InstructorId { get; set; }
    }
}