using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Admin
{
    public class AdminCurrentUserUpdateDto
    {
       
        public string? PhoneNumber { get; set; }
        public string? IdentityId { get; set; }
        public string? CountryCode { get; set; }
        public string? Image { get; set; }

    }
}
