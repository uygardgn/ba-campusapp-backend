namespace BACampusApp.Entities.DbSets
{
    public class Subject : AuditableEntity
    {
		public Subject()
		{
			EducationSubjects = new HashSet<EducationSubject>();
            SupplementaryResourceEducationSubjects = new HashSet<SupplementaryResourceEducationSubject>();
    }
		public string Name { get; set; }
        public string? Description { get; set; }
        //navigation property
        public virtual ICollection<EducationSubject> EducationSubjects { get; set; }

        public virtual ICollection<SupplementaryResourceEducationSubject> SupplementaryResourceEducationSubjects { get; set; }

       
    }
}
