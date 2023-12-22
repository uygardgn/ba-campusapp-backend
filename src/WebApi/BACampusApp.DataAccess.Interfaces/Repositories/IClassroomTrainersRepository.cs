namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IClassroomTrainersRepository : IAsyncRepository, IAsyncInsertableRepository<ClassroomTrainer>, IAsyncFindableRepository<ClassroomTrainer>, IAsyncUpdateableRepository<ClassroomTrainer>, IAsyncDeleteableRepository<ClassroomTrainer>
    {
    }
}
