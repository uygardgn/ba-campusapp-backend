namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class RoleLogRepository : EFBaseRepository<RoleLog>, IRoleLogRepository
    {
        public RoleLogRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
