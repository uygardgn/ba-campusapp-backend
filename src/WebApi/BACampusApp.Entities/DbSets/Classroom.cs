namespace BACampusApp.Entities.DbSets
{
    public class Classroom:AuditableEntity
    {
        public Classroom()
        {
            ClassroomStudents = new HashSet<ClassroomStudent>();
            ClassroomTrainers = new HashSet<ClassroomTrainer>();            
        }
        public Guid EducationId { get; set; }
        public Guid BranchId { get; set; }
        public Guid? TrainingTypeId { get; set; }
        public Guid? GroupTypeId { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public string Name { get; set; }
        public virtual GroupType GroupType { get; set; }

        //Navigation Property
        public virtual Branch Branch { get; set; }
        public virtual Education Education { get; set; }
        public virtual TrainingType TrainingType { get; set; }
        public virtual ICollection<ClassroomStudent> ClassroomStudents { get; set; }
        public virtual ICollection<ClassroomTrainer> ClassroomTrainers { get; set; }

    }
}