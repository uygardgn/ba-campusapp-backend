namespace BACampusApp.Business.Profiles
{
    public class UserPasswordsProfile : Profile
    {
        public UserPasswordsProfile()
        {
            CreateMap<UserPasswords, UserPasswordsListDto>();
            CreateMap<UserPasswordsCreateDto, UserPasswords>();
        }
    }
}
