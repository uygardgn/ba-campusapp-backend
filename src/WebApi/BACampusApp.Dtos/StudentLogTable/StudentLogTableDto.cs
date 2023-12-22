using BACampusApp.Entities.Enums;
namespace BACampusApp.Dtos.StudentLogTable
{
    public class StudentLogTableDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public StudentActionType StudentActionType { get; set; }
    }
}
