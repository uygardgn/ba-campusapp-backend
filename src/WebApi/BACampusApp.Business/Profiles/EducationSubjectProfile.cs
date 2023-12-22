using AutoMapper;
using BACampusApp.Dtos.EducationSubject;
using BACampusApp.Entities.DbSets;

namespace BACampusApp.Business.Profiles
{
    public class EducationSubjectProfile:Profile
    {
        public EducationSubjectProfile()
        {
       
            CreateMap<EducationSubjectCreateDto, EducationSubject>();
            CreateMap<EducationSubject, EducationSubjectDetailDto>();
            CreateMap<EducationSubject, EducationSubjectListDto>();
            CreateMap<EducationSubjectUpdateDto, EducationSubject>().ReverseMap();
            CreateMap<EducationSubject, EducationSubjectDto>();
            CreateMap<EducationSubject, EducationSubjectDeletedListDto>();
            CreateMap<EducationSubject, EducationSubjectsListByEducationIdsDto>();


        }
    }
}
