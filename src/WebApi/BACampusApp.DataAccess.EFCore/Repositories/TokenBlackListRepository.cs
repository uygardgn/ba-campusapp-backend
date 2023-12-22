

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TokenBlackListRepository : EFBaseRepository<TokenBlackList>, ITokenBlackListRepository
    {
        public TokenBlackListRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}
