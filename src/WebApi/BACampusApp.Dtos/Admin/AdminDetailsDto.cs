using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Admin
{
    public class AdminDetailsDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CountryCode { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
    }
}
