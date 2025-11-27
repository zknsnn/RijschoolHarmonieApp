namespace RijschoolHarmonieApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public int? InstructorId { get; set; }

        public ICollection<InstructorPrice> InstructorPrices { get; set; }

        public User(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string passwordHash,
            UserRole role,
            int? instructorId = null
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            Role = role;
            InstructorId = instructorId;
        }

        private User() { }
    }
}
