using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.EducationSubject
{
    public class EducationSubjectsListByEducationIdsDto
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
