using BACampusApp.Dtos.TrainerLogTable;
namespace BACampusApp.Business.Profiles
{
    public class TrainerLogTableProfile : Profile
    {
        public TrainerLogTableProfile()
        {
            CreateMap<TrainerLogTableCreateDto, TrainerLogTable>();
            CreateMap<TrainerLogTableDto, TrainerLogTable>();
        }
    }
}
