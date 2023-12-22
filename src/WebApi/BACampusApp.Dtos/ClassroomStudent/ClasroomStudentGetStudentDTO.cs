using BACampusApp.Dtos.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.ClassroomStudent
{
    public class ClasroomStudentGetStudentDTO
    {
        public List<StudentListDto> HaveClassrooms { get; set; }
        public List<StudentListDto> HaventClassrooms { get; set; }
    }
}
