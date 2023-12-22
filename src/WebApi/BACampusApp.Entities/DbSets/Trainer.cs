namespace BACampusApp.Entities.DbSets
{
    public class Trainer:BaseUser
    {
        public Trainer()
        {

            ClassroomTrainers = new HashSet<ClassroomTrainer>();
        }

        //navigation property
        public virtual ICollection<ClassroomTrainer> ClassroomTrainers { get; set; }

    }
}
