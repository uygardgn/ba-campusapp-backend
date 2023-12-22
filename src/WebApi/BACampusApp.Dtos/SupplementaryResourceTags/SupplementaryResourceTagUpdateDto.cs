using BACampusApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResourceTags
{
    public class SupplementaryResourceTagUpdateDto
    {
        public Guid Id { get; set; }
        public Guid SupplementaryResourceId { get; set; }
        public Guid TagId { get; set; }
    }
}
