namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITechnicalUnitsRepository: IAsyncFindableRepository<TechnicalUnits>, IAsyncInsertableRepository<TechnicalUnits>, IAsyncRepository, IAsyncDeleteableRepository<TechnicalUnits>, IAsyncUpdateableRepository<TechnicalUnits>
    {
    }
}
