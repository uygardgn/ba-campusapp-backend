using AutoMapper;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.ClassroomTrainers;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class ClassroomTrainersProfile:Profile
    {
        public ClassroomTrainersProfile()
        {
            CreateMap<ClassroomTrainer, ClassroomTrainersCreateDto>().ReverseMap();
            CreateMap<ClassroomTrainer, ClassroomTrainersListDto>().ReverseMap();
            CreateMap<ClassroomTrainer, ClassroomTrainersUpdateDto>().ReverseMap();
            CreateMap<ClassroomTrainer, ClassroomTrainerDto>().ReverseMap();
            CreateMap<ClassroomTrainer, ClassroomTrainerActiveUpdateDto>().ReverseMap();
            CreateMap<ClassroomTrainer, ClassromTrainerDeletedListDto>().ReverseMap();
            CreateMap<Trainer, TrainerListByClassroomIdDto>().ReverseMap();

        }
    }
}
