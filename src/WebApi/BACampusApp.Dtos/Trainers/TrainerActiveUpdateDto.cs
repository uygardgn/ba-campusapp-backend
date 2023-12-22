using BACampusApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Trainers
{
    public class TrainerActiveUpdateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
    }
}
