namespace BACampusApp.Entities.DbSets
{
    public class Tag : AuditableEntity
    {
        public Tag()
        {
            SupplementaryResourceTags = new HashSet<SupplementaryResourceTag>();
        }
        public string Name { get; set; }

        //Navigation Properties        
        public virtual ICollection<SupplementaryResourceTag> SupplementaryResourceTags { get; set; }
    }
}