namespace BACampusApp.Entities.DbSets
{
    public class Student : BaseUser
    {
        public Student()
        {

            StudentHomeworks = new HashSet<StudentHomework>();
            ClassroomStudents = new HashSet<ClassroomStudent>();

        }
        //Navigation property

        public virtual ICollection<ClassroomStudent> ClassroomStudents { get; set; }
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }



    }
}
