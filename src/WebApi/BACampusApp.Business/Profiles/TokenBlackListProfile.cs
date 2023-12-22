using BACampusApp.Dtos.TokenBlackList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class TokenBlackListProfile : Profile
    {
        public TokenBlackListProfile()
        {
            CreateMap<TokenBlackListCreateDto, TokenBlackList>();
        }

    }
}
