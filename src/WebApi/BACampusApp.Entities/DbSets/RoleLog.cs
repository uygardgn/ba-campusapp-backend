namespace BACampusApp.Entities.DbSets
{
    public class RoleLog : BaseEntity
    {
        public string ActiveRole { get; set; }
        public ActionType ActionType { get; set; }
    }
}
