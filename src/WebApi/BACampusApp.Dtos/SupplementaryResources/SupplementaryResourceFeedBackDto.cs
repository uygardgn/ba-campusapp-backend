using BACampusApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceFeedBackDto
    {
        public Guid Id { get; set; }
        public string? Feedback { get; set; }

        public ResourcesTypeStatus ResourcesTypeStatus { get; set; } = ResourcesTypeStatus.Rejected;
    }
}
