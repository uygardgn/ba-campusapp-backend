namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IActivityStateLogRepository : IAsyncInsertableRepository<ActivityStateLog>, IAsyncRepository, IAsyncFindableRepository<ActivityStateLog>, IAsyncUpdateableRepository<ActivityStateLog>, IAsyncDeleteableRepository<ActivityStateLog>
    {

    }
}
