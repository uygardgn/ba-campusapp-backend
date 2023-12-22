using AutoMapper;
using BACampusApp.Dtos.ActivityStateLog;
using BACampusApp.Dtos.Classroom;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.ClassroomTrainers;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Trainers;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class ActivityStateLogProfile : Profile
    {
        public ActivityStateLogProfile()
        {
            CreateMap<ActivityStateLogCreateDto, ActivityStateLog>().ReverseMap()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.ModifiedBy));

            CreateMap<ActivityStateLogCreateDto, StudentActiveUpdateDto>().ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

            CreateMap<ActivityStateLogCreateDto, ClassroomActiveUpdateDto>().ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

            CreateMap<ActivityStateLogCreateDto, TrainerActiveUpdateDto>().ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

            CreateMap<ActivityStateLogCreateDto, ClassroomStudentActiveUpdateDto>().ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

            CreateMap<ActivityStateLogCreateDto, ClassroomTrainerActiveUpdateDto>().ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

            CreateMap<ActivityStateLogListDto, ActivityStateLog>().ReverseMap();

            CreateMap<ActivityStateLogDto, ActivityStateLog>().ReverseMap();




        }
    }
}
