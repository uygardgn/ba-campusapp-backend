namespace BACampusApp.Business.Profiles
{
    public class RoleLogProfile : Profile
    {
        public RoleLogProfile()
        {
            CreateMap<RoleLogCreateDto, RoleLog>().ReverseMap();

            CreateMap<RoleLogDto, RoleLog>().ReverseMap();
        }
    }
}
