using AutoMapper;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.Trainers;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class ClassroomStudentProfile:Profile
    {
        public ClassroomStudentProfile()
        {
            CreateMap<ClassroomStudent, ClassroomStudentCreateDto>().ReverseMap();
            CreateMap<ClassroomStudent, ClassroomStudentUpdateDto>().ReverseMap();
            CreateMap<ClassroomStudent, ClassroomStudentListDto>().ReverseMap();
            CreateMap<ClassroomStudent, ClassroomStudentDto>().ReverseMap();
            CreateMap<ClassroomStudent, ClassroomStudentActiveUpdateDto>().ReverseMap();
            CreateMap<ClassroomStudent, ClassromStudentDeletedListDto>().ReverseMap();
            CreateMap<ClassroomStudent, StudentListByClassroomIdDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.Student.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.Student.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Student.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(x => x.Student.PhoneNumber))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Student.Image));

            CreateMap<ClassroomStudent, ActiveClassroomByEducationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.ClassroomId))
                .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(x => x.Classroom.Name))
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(x => x.Classroom.EducationId))
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(x => x.Classroom.Education.Name))
                .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(x => x.Classroom.OpenDate))
                .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(x => x.Classroom.ClosedDate))
                .ForMember(dest => dest.GroupTypeId, opt => opt.MapFrom(x => x.Classroom.GroupTypeId))
                .ForMember(dest => dest.GroupTypeName, opt => opt.MapFrom(x => x.Classroom.GroupType.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Classroom.Status))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(x => x.Classroom.BranchId))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(x => x.Classroom.Branch.Name))
                .ForMember(dest => dest.TrainingTypeId, opt => opt.MapFrom(x => x.Classroom.TrainingTypeId))
                .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(x => x.Classroom.TrainingType.Name));




        }
    }
}
