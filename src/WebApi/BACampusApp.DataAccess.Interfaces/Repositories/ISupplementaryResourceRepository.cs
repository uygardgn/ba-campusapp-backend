namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ISupplementaryResourceRepository : IAsyncFindableRepository<SupplementaryResource>, IAsyncInsertableRepository<SupplementaryResource>, IAsyncRepository, IAsyncUpdateableRepository<SupplementaryResource>,
        IAsyncDeleteableRepository<SupplementaryResource>
    {
        
    }
}
