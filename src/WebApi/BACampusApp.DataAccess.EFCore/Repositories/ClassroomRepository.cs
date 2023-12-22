namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class ClassroomRepository : EFBaseRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
