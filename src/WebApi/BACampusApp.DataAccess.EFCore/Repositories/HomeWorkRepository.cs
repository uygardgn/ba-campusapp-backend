namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class HomeWorkRepository : EFBaseRepository<HomeWork>, IHomeWorkRepository
    {
        public HomeWorkRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
