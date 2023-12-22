namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class UserPasswordsRepository : EFBaseRepository<UserPasswords>, IUserPasswordsRepository
    {
        public UserPasswordsRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
