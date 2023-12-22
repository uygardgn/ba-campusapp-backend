using BACampusApp.Dtos.StudentLogTable;
namespace BACampusApp.Business.Profiles
{
    public class StudentLogTableProfile : Profile
    {
        public StudentLogTableProfile()
        {
            CreateMap<StudentLogTableCreateDto, StudentLogTable>();
            CreateMap<StudentLogTableDto, StudentLogTable>();
        }
    }
}
