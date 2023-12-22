namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ICommentRepository : IAsyncRepository, IAsyncInsertableRepository<Comment>, IAsyncFindableRepository<Comment>, IAsyncUpdateableRepository<Comment>,IAsyncDeleteableRepository<Comment>
    {
    }
}
