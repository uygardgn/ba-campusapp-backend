namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ICategoryRepository : IAsyncFindableRepository<Category>, IAsyncInsertableRepository<Category>, IAsyncRepository, IAsyncDeleteableRepository<Category>, IAsyncUpdateableRepository<Category>
    {
        
    }
}
