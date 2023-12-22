namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IStudentHomeworkRepository : IAsyncRepository, IAsyncInsertableRepository<StudentHomework>, IAsyncFindableRepository<StudentHomework>, IAsyncUpdateableRepository<StudentHomework>, IAsyncDeleteableRepository<StudentHomework>
    {

    }
}
