namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ISubjectRepository : IAsyncFindableRepository<Subject>, IAsyncInsertableRepository<Subject>, IAsyncRepository, IAsyncUpdateableRepository<Subject>,IAsyncDeleteableRepository<Subject>
    {

    }
}
