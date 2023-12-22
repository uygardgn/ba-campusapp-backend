namespace BACampusApp.Business.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentCreateDto>();
            CreateMap<Student, StudentListDto>();
            CreateMap<StudentCurrentUserUpdateDto, Student>();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();
            CreateMap<Student, StudentDto>();
            CreateMap<Student, StudentDetailsDto>();
            CreateMap<Student, StudentActiveUpdateDto>().ReverseMap();
            CreateMap<Student, StudentDeletedListDto>();
            CreateMap<Student, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<StudentCreateDto, IdentityUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<StudentUpdateDto, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
