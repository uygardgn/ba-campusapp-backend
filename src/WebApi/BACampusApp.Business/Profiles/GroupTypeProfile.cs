using BACampusApp.Dtos.GroupType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class GroupTypeProfile:Profile
    {
        public GroupTypeProfile()
        {
            CreateMap<GroupTypeCreateDto, Entities.DbSets.GroupType>();
            CreateMap<Entities.DbSets.GroupType, GroupTypeDto>();
            CreateMap<GroupTypeUpdateDto, Entities.DbSets.GroupType>();
            CreateMap<Entities.DbSets.GroupType, GroupTypeDeleteListDto>();
        }
    }
}
