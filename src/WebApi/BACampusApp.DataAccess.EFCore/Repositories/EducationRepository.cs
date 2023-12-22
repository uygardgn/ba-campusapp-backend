namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class EducationRepository : EFBaseRepository<Education>, IEducationRepository
    {
        public EducationRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
