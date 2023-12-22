namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TrainerRepository: EFBaseRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(BACampusAppDbContext context) : base(context)
        {

        }

        public Task<Trainer> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
        }
    }
}
