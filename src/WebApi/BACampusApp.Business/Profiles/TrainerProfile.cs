namespace BACampusApp.Business.Profiles
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<TrainerCreateDto, Trainer>();
            CreateMap<Trainer,TrainerListDto>();
            CreateMap<Trainer, TrainerUpdateDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>();
            CreateMap<TrainerCurrentUserUpdateDto , Trainer>();
            CreateMap<Trainer, TrainerDeletedListDto>();
            CreateMap<Trainer, TrainerDetailsDto>();
            CreateMap<Trainer, TrainerActiveUpdateDto>().ReverseMap();
            CreateMap<Trainer, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<TrainerCreateDto, IdentityUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<TrainerUpdateDto, IdentityUser>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
