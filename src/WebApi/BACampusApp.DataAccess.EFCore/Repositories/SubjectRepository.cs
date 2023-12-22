namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class SubjectRepository : EFBaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
