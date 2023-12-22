namespace BACampusApp.Entities.DbSets
{
    public class SupplementaryResourceTag : BaseEntity
	{
		public Guid SupplementaryResourceId { get; set; }
		public Guid TagId { get; set; }

		public virtual SupplementaryResource SupplementaryResources { get; set; }
		public virtual Tag Tag { get; set; }
	}
}