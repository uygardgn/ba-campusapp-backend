

namespace BACampusApp.Dtos.EducationSubject
{
    public class EducationSubjectCreateDto
    {
        public Guid EducationId { get; set; }
        public Guid SubjectId { get; set; }
        public int? Order { get; set; }
    }
}
