namespace BACampusApp.Entities.DbSets
{
    public class ClassroomTrainer : AuditableEntity
    {
        public ClassroomTrainer() 
        {

        }

        // Trainer FK
        public Guid TrainerId { get; set; }
       
        // Classroom FK
        public Guid ClassroomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        // Classroom Navigation Property
        public virtual Classroom Classroom { get; set; }
		// Trainer Navigation Property
		public virtual Trainer Trainer { get; set; }
	}
}
