using BACampusApp.Dtos.HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkCreateDto
    {
        
        public Guid HomeworkId { get; set; } 

        public Guid? StudentId { get; set; }
    }
}
