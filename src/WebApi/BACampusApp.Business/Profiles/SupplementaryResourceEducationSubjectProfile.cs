using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class SupplementaryResourceEducationSubjectProfile : Profile
    {
        public SupplementaryResourceEducationSubjectProfile()
        {

            CreateMap<SupplementaryResourceEducationSubject, SupplementaryResourceEducationSubjectCreateDto>();
            CreateMap<SupplementaryResourceEducationSubject, SupplementaryResourceEducationSubjectUpdateDto>();
            CreateMap<SupplementaryResourceEducationSubject, SupplementaryResourceEducationSubjectListDto>()
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(x => x.Educations.Name))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(x => x.Subjects.Name));
            CreateMap<SupplementaryResourceEducationSubject, SupplementaryResourceEducationSubjectDto>();
            CreateMap<SupplementaryResourceEducationSubject, SupplementaryResourceEducationSubjectDeleteListDto>();
        }

    }
}


