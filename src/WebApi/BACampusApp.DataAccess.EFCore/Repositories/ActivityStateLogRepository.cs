namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class ActivityStateLogRepository : EFBaseRepository<ActivityStateLog>, IActivityStateLogRepository
    {
        public ActivityStateLogRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
