
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using BACampusApp.Dtos.SupplementaryResourceTags;
using BACampusApp.Entities.DbSets;
using System.Security.AccessControl;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }
        //public Guid EducationId { get; set; }
        //public Guid SubjectId { get; set; }
        public string FileURL { get; set; }
        public virtual ICollection<SupplementaryResourceTagListDto> SupplementaryResourceTags { get; set; }
        public virtual ICollection<SupplementaryResourceEducationSubjectListDto> SupplementaryResourceEducationSubjects { get; set; }


    }
}
