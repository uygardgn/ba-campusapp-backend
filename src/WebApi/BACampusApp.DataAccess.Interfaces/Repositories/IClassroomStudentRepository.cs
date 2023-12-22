namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IClassroomStudentRepository : IAsyncRepository, IAsyncInsertableRepository<ClassroomStudent>, IAsyncFindableRepository<ClassroomStudent>, IAsyncUpdateableRepository<ClassroomStudent>, IAsyncDeleteableRepository<ClassroomStudent>
    {
    }
}
