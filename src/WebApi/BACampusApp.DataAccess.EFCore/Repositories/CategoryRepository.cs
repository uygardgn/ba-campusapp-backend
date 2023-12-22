namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class CategoryRepository : EFBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
