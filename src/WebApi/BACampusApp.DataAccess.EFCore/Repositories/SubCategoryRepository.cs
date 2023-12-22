namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class SubCategoryRepository : EFBaseRepository<Category>, ISubCategoryRepository
    {
        public SubCategoryRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
