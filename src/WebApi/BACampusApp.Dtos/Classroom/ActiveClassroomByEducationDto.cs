using BACampusApp.Core.Enums;
using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Classroom
{
    public class ActiveClassroomByEducationDto
    {
        public Guid Id { get; set; }
        public Guid ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public Guid EducationId { get; set; }
        public string EducationName { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Guid GroupTypeId { get; set; }
        public string? GroupTypeName { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid TrainingTypeId { get; set; }
        public string? TrainingTypeName { get; set; }
        public Status Status { get; set; }

    }
}
