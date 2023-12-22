namespace BACampusApp.Entities.DbSets
{
    public class Branch : AuditableEntity
    {
        public Branch()
        {
            Classrooms = new HashSet<Classroom>();
        }
        public string Name { get; set; }

        //Navigation Property

        public virtual ICollection<Classroom> Classrooms { get; set; }

    }
}
