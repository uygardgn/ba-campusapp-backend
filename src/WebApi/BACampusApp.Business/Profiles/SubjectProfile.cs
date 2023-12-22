using AutoMapper;
using BACampusApp.Dtos.Educations;
using BACampusApp.Dtos.Subjects;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectCreateDto, Subject>();
            CreateMap<Subject, SubjectDetailsDto>();
            CreateMap<Subject, SubjectListDto>();
            CreateMap<SubjectUpdateDto, Subject>().ReverseMap();
            CreateMap<Subject, SubjectDto>();
            CreateMap<Subject, SubjectDeletedListDto>();


        }
    }
}
