using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceDeletedListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ResourceType { get; set; }

        public string SubjectId { get; set; }
        public string FileURL { get; set; }
    }
}
