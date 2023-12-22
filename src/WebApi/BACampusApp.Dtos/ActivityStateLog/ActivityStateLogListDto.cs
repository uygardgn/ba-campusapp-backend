using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.ActivityStateLog
{
    public class ActivityStateLogListDto
    {
        public Guid Id { get; set; }
        public string ItemId { get; set; }
        public string Description { get; set; }

    }
}
