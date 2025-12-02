using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.DTOs.User
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } // Required
        public string LastName { get; set; } // Required
        public string Email { get; set; } // Required
        public string PhoneNumber { get; set; } // Optional
        public string PasswordHash { get; set; } // Required
        public UserRole Role { get; set; } // Required
        public int? InstructorId { get; set; } // Optional
    }
}



