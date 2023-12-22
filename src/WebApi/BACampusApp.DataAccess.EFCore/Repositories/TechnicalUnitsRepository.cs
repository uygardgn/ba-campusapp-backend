namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TechnicalUnitsRepository : EFBaseRepository<TechnicalUnits>, ITechnicalUnitsRepository
    {
        public TechnicalUnitsRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
