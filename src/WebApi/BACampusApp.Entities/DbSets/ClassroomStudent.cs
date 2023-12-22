 namespace BACampusApp.Entities.DbSets
{
    public class ClassroomStudent : AuditableEntity
    {
        public ClassroomStudent()
        {

        }

        // Student FK
        public Guid StudentId { get; set; }
        // Student Navigation Property
        public virtual Student Student { get; set; }
        // Classroom FK
        public Guid ClassroomId { get; set; }
        // Classroom Navigation Property
        public virtual Classroom Classroom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
