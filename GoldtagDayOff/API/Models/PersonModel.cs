using API.Entities;

namespace API.Models
{
    public class PersonModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }
        public string Unvan { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }
    }
}
