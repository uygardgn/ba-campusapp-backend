using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkGetDto
    {
        public Guid HomeWorkId { get; set; }
        public Guid StudentId { get; set; }
    }
}
