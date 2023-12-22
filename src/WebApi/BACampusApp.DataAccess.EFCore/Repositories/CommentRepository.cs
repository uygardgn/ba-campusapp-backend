namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class CommentRepository : EFBaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
