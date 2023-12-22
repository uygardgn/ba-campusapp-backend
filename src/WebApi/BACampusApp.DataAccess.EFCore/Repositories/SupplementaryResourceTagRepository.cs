namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class SupplementaryResourceTagRepository : EFBaseRepository<SupplementaryResourceTag>, ISupplementaryResourceTagRepository
    {
        public SupplementaryResourceTagRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
