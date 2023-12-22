namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class SupplementaryResourceRepository : EFBaseRepository<SupplementaryResource>, ISupplementaryResourceRepository
    {
        public SupplementaryResourceRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
