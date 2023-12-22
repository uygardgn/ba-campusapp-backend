namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class ClassroomStudentRepository : EFBaseRepository<ClassroomStudent>, IClassroomStudentRepository
    {
        public ClassroomStudentRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
