﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Trainers
{
    public class TrainerUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Address { get; set; }
    }
}
