namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TagRepository : EFBaseRepository<Tag>, ITagRepository
    {
        public TagRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
