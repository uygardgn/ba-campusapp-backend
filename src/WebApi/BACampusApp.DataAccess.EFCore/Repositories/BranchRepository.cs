namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class BranchRepository : EFBaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
