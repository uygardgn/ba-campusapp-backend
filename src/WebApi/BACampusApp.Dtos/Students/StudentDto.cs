using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public bool Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ClassroomName { get; set; } = null!;
        public string? Image { get; set; }
        public string? Address { get; set; }

    }
}
