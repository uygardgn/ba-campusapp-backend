using AutoMapper;
using BACampusApp.Dtos.Account;
using BACampusApp.Dtos.Admin;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<LoginDto, Admin>();
            CreateMap<Admin, LoginDto>();
        }
    }
}
