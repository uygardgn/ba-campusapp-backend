
namespace BACampusApp.Dtos.EducationSubject
{
    public class EducationSubjectUpdateDto
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
