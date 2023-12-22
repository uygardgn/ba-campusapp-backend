using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using BACampusApp.Dtos.SupplementaryResourceTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceDeletedDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SupplementaryResourceTagListDto> SupplementaryResourceTags { get; set; }
        public virtual ICollection<SupplementaryResourceEducationSubjectListDto> SupplementaryResourceEducationSubjects { get; set; }
    }
}
