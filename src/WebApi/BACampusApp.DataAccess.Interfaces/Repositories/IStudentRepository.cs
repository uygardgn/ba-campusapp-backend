namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface  IStudentRepository: IAsyncRepository, IAsyncTransactionRepository, IAsyncInsertableRepository<Student>, IAsyncFindableRepository<Student>, IAsyncUpdateableRepository<Student>, IAsyncDeleteableRepository<Student>,IAsyncIdentityRepository<Student>
	{
    }
}
