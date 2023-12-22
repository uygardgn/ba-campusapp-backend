namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(BACampusAppDbContext context) : base(context)
        {
        }

        public Task<Admin> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x=>x.IdentityId == identityId);
        }
    }
}
