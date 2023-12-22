namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IEducationSubjectRepository : IAsyncFindableRepository<EducationSubject>, IAsyncInsertableRepository<EducationSubject>, IAsyncRepository,IAsyncUpdateableRepository<EducationSubject>,IAsyncDeleteableRepository<EducationSubject>
    {
    }
}
