using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects
{
    public class SupplementaryResourceEducationSubjectCreateDto
    {
        public Guid EducationId { get; set; }
        public List<Guid> SubjectId { get; set; }

        public Guid SupplementaryResource { get; set; }
    }

}
