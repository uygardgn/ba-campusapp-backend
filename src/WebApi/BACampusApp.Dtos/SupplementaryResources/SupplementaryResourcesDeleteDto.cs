using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourcesDeleteDto
    {
        public Guid Id { get; set; }
        public bool IsHardDelete { get; set; }
    }
}
