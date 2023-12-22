using AutoMapper;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class StudentHomeworkProfile : Profile
    {
        public StudentHomeworkProfile()
        {
            CreateMap<StudentHomeworkCreateDto, StudentHomework>().ReverseMap();
            CreateMap<StudentHomework, StudentHomeworkDto>()
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.ClassroomId, opt => opt.MapFrom(src => src.HomeWork.ClassroomId))
                .ReverseMap();
            CreateMap<StudentHomework, StudentHomeworkPointDto>().ReverseMap();
			CreateMap<StudentHomeworkTrainerUpdateDto, StudentHomework>().ForMember(dest => dest.AttachedFile, opt =>
			{
				opt.MapFrom<StudentHomeworkTrainerUpdateResolver>();
			});
			CreateMap<StudentHomework, StudentHomeworkListDto>().ReverseMap();
            CreateMap<StudentHomeworkStudentUpdateDto, StudentHomework>().ForMember(dest => dest.AttachedFile, opt =>
            {
                opt.MapFrom<StudentHomeworkStudentUpdateResolver>();
            }).ReverseMap();
            CreateMap<StudentHomework, StudentHomeWorkDeletedListDto>().ReverseMap();
            CreateMap<StudentHomework, StudentListByHomeworkIdDto>()
                .ForMember(dest => dest.StudentHomeworkId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.Student.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.Student.LastName));
            CreateMap<StudentHomework, StudentHomeworkResponseDto>().ReverseMap();
            CreateMap<StudentHomework, StudentHomeworkFeedbackDto>().ReverseMap();
        }
    }


	public class StudentHomeworkTrainerUpdateResolver : IValueResolver<StudentHomeworkTrainerUpdateDto, StudentHomework, string>
	{
		public string Resolve(StudentHomeworkTrainerUpdateDto source, StudentHomework destination, string destMember, ResolutionContext context)
		{
			return (source.IsFileChanged && source.AttachedFile != null) ? destination.AttachedFile : destination.AttachedFile;
		}
	}


    public class StudentHomeworkStudentUpdateResolver : IValueResolver<StudentHomeworkStudentUpdateDto, StudentHomework, string>
    {
        /// <summary>
        /// Resolve metodu studentHomeworkUpdateDto ve studenthomework nesneleri maplenirken attachedFile tipleri birbirinden farklı (IFormFile ve string) olduğu için düzenlenmiş bir çözümleme metodur.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns>Eğer source daki AttachedFile boş değilse destinationun Attached File'ı döner eğer boş ise empty değer döner</returns>
        public string Resolve(StudentHomeworkStudentUpdateDto source, StudentHomework destination, string destMember, ResolutionContext context)
        {
            return (source.AttachedFile != null) ? destination.AttachedFile : destination.AttachedFile;
        }
    }
}
