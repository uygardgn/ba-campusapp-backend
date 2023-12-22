using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.ActivityStateLog
{
    public class ActivityStateLogCreateDto
    {
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
