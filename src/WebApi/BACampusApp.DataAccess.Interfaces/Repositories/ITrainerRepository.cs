namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITrainerRepository: IAsyncRepository, IAsyncTransactionRepository, IAsyncInsertableRepository<Trainer>, IAsyncFindableRepository<Trainer>, IAsyncUpdateableRepository<Trainer>, IAsyncDeleteableRepository<Trainer>,IAsyncIdentityRepository<Trainer>
    {
    }
}
