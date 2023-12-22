using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Classroom
{
    public class ClassroomTechnicDto
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string EducationName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid BranchId { get; set; }
        public Guid TrainingTypeId { get; set; }
    }
}
