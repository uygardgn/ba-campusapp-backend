using AutoMapper;
using BACampusApp.Dtos.Classroom;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class ClassroomProfile : Profile
    {
        public ClassroomProfile()
        {
            CreateMap<ClassroomCreateDto, Classroom>().ReverseMap();
            CreateMap<Classroom, ClassroomListDto>();
            CreateMap<Classroom, ClassroomUpdateDto>().ReverseMap();
            CreateMap<Classroom, ClassroomDto>()
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(x => x.EducationId))
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(x => x.Education.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(x => x.Education.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Education.SubCategoryId));

            CreateMap<Classroom, ClassroomTechnicDto>()
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(x => x.EducationId))
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(x => x.Education.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(x => x.Education.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Education.SubCategoryId));
            CreateMap<Classroom, ClassroomActiveUpdateDto>().ReverseMap();
            CreateMap<Classroom, ClassromDeletedListDto>();
            CreateMap<Classroom, ClassroomListByTrainerIdDTO>().ReverseMap();

            CreateMap<Classroom, ActiveClassroomByEducationDto>()
                .ForMember(dest => dest.ClassroomId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(src => src.Education.Name))
                .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.TrainingType.Name));

        }
    }
}

