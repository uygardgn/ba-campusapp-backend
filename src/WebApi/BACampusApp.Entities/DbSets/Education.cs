namespace BACampusApp.Entities.DbSets
{
    public class Education : AuditableEntity
    {
		public Education()
		{
			EducationSubjects = new HashSet<EducationSubject>();
			Classrooms = new HashSet<Classroom>();
            SupplementaryResourceEducationSubjects = new HashSet<SupplementaryResourceEducationSubject>();

    }
		public string Name { get; set; }
		public int CourseHours { get; set; }
		public string Description { get; set; }

		//Category FK
		public Guid SubCategoryId { get; set; }
		//Navigation properies
		public virtual Category? Category { get; set; }
		public virtual ICollection<EducationSubject> EducationSubjects { get; set; }
        public virtual ICollection<SupplementaryResourceEducationSubject> SupplementaryResourceEducationSubjects { get; set; }
		public virtual ICollection<Classroom> Classrooms { get; set; }

	}
}
