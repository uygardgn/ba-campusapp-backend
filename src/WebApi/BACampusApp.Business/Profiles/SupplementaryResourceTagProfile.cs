using AutoMapper;
using BACampusApp.Dtos.SupplementaryResourceTags;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class SupplementaryResourceTagProfile:Profile
    {
        public SupplementaryResourceTagProfile()
        {
            CreateMap<SupplementaryResourceTag, SupplementaryResourceTagCreateDto>().ReverseMap();
            CreateMap<SupplementaryResourceTag, SupplementaryResourceTagUpdateDto>().ReverseMap();
            CreateMap<SupplementaryResourceTag, SupplementaryResourceTagListDto>().ForMember(dest => dest.TagName, opt => opt.MapFrom(x => x.Tag.Name)).ReverseMap();
            CreateMap<SupplementaryResourceTag, SupplementaryResourceTagDto>().ReverseMap();
            CreateMap<SupplementaryResourceTag, SupplementaryResourceTagDeletedListDto>().ReverseMap();

        }
    }
}
