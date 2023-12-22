namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IHomeWorkRepository: IAsyncFindableRepository<HomeWork>, IAsyncInsertableRepository<HomeWork>, IAsyncRepository, IAsyncDeleteableRepository<HomeWork>, IAsyncUpdateableRepository<HomeWork>
    {
    }
}
