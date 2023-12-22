using AutoMapper;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Tag;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagCreateDto, Tag>();
            CreateMap<Tag, TagDetailsDto>();
            CreateMap<Tag, TagListDto>();
            CreateMap<TagUpdateDto, Tag>().ReverseMap();
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, TagDeletedListDto>();

        }
    }
}