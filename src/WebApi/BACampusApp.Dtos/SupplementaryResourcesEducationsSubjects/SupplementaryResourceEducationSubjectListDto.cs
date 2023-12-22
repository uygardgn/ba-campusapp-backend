using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects
{
    public class SupplementaryResourceEducationSubjectListDto
    {
        public Guid Id { get; set; }
        public Guid SupplementaryResourceId { get; set; }
        public Guid EducationId { get; set; }
        public string EducationName { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? Order { get; set; }
    }
}
