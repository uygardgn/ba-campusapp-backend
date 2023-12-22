using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Classroom
{
    public class ClassromDeletedListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid EducationId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Guid GroupTypeId { get; set; }
    }
}
