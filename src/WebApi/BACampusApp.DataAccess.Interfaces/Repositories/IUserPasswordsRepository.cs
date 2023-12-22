namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IUserPasswordsRepository : IAsyncRepository, IAsyncInsertableRepository<UserPasswords>, IAsyncFindableRepository<UserPasswords>, IAsyncQueryableRepository<UserPasswords>, IAsyncUpdateableRepository<UserPasswords>, IAsyncDeleteableRepository<UserPasswords>
    {

    }
}
