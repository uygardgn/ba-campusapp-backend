namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IEducationRepository : IAsyncFindableRepository<Education>, IAsyncInsertableRepository<Education>, IAsyncRepository,IAsyncDeleteableRepository<Education>, IAsyncUpdateableRepository<Education>

	
    {

    }
}
