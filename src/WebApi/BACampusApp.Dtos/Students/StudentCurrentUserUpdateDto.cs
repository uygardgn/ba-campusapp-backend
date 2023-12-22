using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Students
{
    public class StudentCurrentUserUpdateDto
    {
        public string? PhoneNumber { get; set; }
        public string? IdentityId { get; set; }
        public string? CountryCode { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
    }
}
