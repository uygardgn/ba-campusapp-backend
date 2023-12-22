namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class StudentHomeworkRepository : EFBaseRepository<StudentHomework>, IStudentHomeworkRepository
    {
        public StudentHomeworkRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
