namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IBranchRepository : IAsyncRepository, IAsyncInsertableRepository<Branch>, IAsyncFindableRepository<Branch>, IAsyncUpdateableRepository<Branch>, IAsyncDeleteableRepository<Branch>
    {
    }
}
