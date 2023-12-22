namespace BACampusApp.Entities.DbSets
{
    public class TechnicalUnits : AuditableEntity
    {
        public TechnicalUnits()
        {
            Categories = new HashSet<Category>();
        }

        public string Name { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
