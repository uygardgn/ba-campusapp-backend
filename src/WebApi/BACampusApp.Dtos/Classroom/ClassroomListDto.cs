using BACampusApp.Core.Enums;
using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Classroom
{
    public class ClassroomListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid EducationId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Guid GroupTypeId { get; set; }
        public Status Status { get; set; }
        public Guid BranchId { get; set; }
        public Guid TrainingTypeId { get; set; }

    }
}
