using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.ActivityStateLog
{
    public class ActivityStateDetailsDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
