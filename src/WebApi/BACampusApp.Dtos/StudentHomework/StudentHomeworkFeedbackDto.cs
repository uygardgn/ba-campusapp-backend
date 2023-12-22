using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkFeedbackDto
    {
        public Guid Id { get; set; }
        public string? Feedback { get; set; }
    }
}
