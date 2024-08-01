
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
        public class Person: BaseEntity
        {
            public int PersonId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string TelNo { get; set; }
            public string Unvan { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
            public UserRole Role { get; set; }
        }

        public enum UserRole
        {
            Admin = 1,
            User = 2
        }
    }

