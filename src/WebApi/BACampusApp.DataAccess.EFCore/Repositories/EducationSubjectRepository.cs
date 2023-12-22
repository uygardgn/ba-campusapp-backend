namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class EducationSubjectRepository : EFBaseRepository<EducationSubject>, IEducationSubjectRepository
    {
        public EducationSubjectRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
