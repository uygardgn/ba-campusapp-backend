using BACampusApp.Entities.Enums;
namespace BACampusApp.Dtos.StudentLogTable
{
    public class StudentLogTableCreateDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentActionType StudentActionType { get; set; }
    }
}
