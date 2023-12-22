namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IClassroomRepository: IAsyncRepository, IAsyncInsertableRepository<Classroom>, IAsyncFindableRepository<Classroom>, IAsyncUpdateableRepository<Classroom>, IAsyncDeleteableRepository<Classroom>
    {
    }
}
