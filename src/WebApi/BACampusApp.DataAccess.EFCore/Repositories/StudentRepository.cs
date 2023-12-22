namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class StudentRepository : EFBaseRepository<Student>,IStudentRepository
    {
        
        public StudentRepository(BACampusAppDbContext context):base(context) 
        {
            
        }

        public Task<Student> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
        }
    }
}
