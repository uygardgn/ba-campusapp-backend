using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class TrainerLogTable : BaseEntity
    {
        public Guid TrainerId { get; set; }
        public string? Description { get; set; }
        public virtual Trainer Trainer { get; set; } 
        public TrainerActionType TrainerActionType { get; set; }
    }
}
