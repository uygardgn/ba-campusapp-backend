using BACampusApp.Dtos.TrainingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class TrainingTypeProfile : Profile
    {
        public TrainingTypeProfile() 
        {
            CreateMap<TrainingTypeCreateDto, TrainingType>();
            CreateMap<TrainingType, TrainingTypeDto>();
            CreateMap<TrainingTypeUpdateDto, TrainingType>();
            CreateMap<TrainingType, TrainingTypeDeletedListDto>();
        }
    }
}
