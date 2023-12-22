using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResourceTags
{
    public class SupplementaryResourceTagCreateDto
    {
        public Guid SupplementaryResourceId { get; set; }
        public Guid TagId { get; set; }
    }
}
