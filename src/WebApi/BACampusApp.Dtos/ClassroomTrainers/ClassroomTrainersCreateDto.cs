using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.ClassroomTrainers
{
    public class ClassroomTrainersCreateDto
    {
        public Guid TrainerId { get; set; }
        public Guid ClassroomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}
