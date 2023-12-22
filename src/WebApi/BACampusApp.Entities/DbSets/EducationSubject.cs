namespace BACampusApp.Entities.DbSets
{
    public class EducationSubject : AuditableEntity
    {
        public EducationSubject()
        {
            SupplementaryResources = new HashSet<SupplementaryResource>();
        }
        //Subject FK
        public Guid SubjectId { get; set; }
		//Subject navigation property
		public virtual Subject Subject { get; set; }
		//Education FK
		public Guid EducationId { get; set; }
		//Education navigation property
		public virtual Education Education { get; set; }
        public virtual ICollection<SupplementaryResource> SupplementaryResources { get; set; }
        public int? Order { get; set; }
    }
}