using BACampusApp.Core.Enums;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using BACampusApp.Dtos.SupplementaryResourceTags;
using BACampusApp.Entities.DbSets;
using BACampusApp.Entities.Enums;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }
        public string MimeType { get; set; }
       
        public string FileURL { get; set; }

        public virtual ICollection<SupplementaryResourceEducationSubjectListDto> SupplementaryResourceEducationSubjects { get; set; }

        public ResourcesTypeStatus ResourcesTypeStatus { get; set; }
        public virtual ICollection<SupplementaryResourceTagListDto> SupplementaryResourceTags { get; set; }

    }
}