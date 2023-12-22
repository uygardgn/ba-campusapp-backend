namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ISubCategoryRepository : IAsyncFindableRepository<Category>, IAsyncInsertableRepository<Category>, IAsyncRepository, IAsyncDeleteableRepository<Category>, IAsyncUpdateableRepository<Category>
    {
    }
}
