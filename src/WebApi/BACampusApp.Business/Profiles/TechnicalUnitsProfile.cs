using AutoMapper;
using BACampusApp.Dtos.TechnicalUnits;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class TechnicalUnitsProfile : Profile
    {
        public TechnicalUnitsProfile()
        {
            CreateMap<TUnitCreateDto, TechnicalUnits>();
            CreateMap<TechnicalUnits, TUnitDetailsDto>();
            CreateMap<TechnicalUnits, TUnitListDto>();
            CreateMap<TUnitUpdateDto, TechnicalUnits>().ReverseMap();
            CreateMap<TechnicalUnits, TUnitDto>();
            CreateMap<TechnicalUnits, TUnitDeletedListDto>();

        }
    }
}
