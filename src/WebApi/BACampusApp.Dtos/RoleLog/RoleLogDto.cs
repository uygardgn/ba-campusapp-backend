using BACampusApp.Entities.Enums;
namespace BACampusApp.Dtos.RoleLog
{
    public class RoleLogDto
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActiveRole { get; set; }
        public ActionType ActionType { get; set; }
    }
}
