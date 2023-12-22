namespace BACampusApp.Entities.DbSets
{
    public class ActivityStateLog: AuditableEntity
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
    }
}
