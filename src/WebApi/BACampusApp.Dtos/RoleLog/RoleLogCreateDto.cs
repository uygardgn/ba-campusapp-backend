using BACampusApp.Entities.Enums;
namespace BACampusApp.Dtos.RoleLog
{
    public class RoleLogCreateDto
    {
        public string ActiveRole { get; set; }
        public ActionType ActionType { get; set; }
    }
}
