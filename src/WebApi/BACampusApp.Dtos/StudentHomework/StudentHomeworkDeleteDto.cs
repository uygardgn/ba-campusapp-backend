using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkDeleteDto
    {
        public Guid id { get; set; }
        public bool IsHardDelete { get; set; }
    }
}
