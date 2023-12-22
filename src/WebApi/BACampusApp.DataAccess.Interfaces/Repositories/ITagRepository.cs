namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITagRepository : IAsyncInsertableRepository<Tag>, IAsyncFindableRepository<Tag>, IAsyncRepository, IAsyncDeleteableRepository<Tag>, IAsyncUpdateableRepository<Tag>
    {

    }
}
