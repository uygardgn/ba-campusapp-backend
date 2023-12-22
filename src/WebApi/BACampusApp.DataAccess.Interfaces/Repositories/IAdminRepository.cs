namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IAdminRepository : IAsyncRepository, IAsyncTransactionRepository, IAsyncInsertableRepository<Admin>, IAsyncFindableRepository<Admin>,IAsyncUpdateableRepository<Admin>,IAsyncDeleteableRepository<Admin>,IAsyncIdentityRepository<Admin>
    {

    }
}
