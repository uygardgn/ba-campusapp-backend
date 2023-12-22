using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class SupplementaryResourceEducationSubject : BaseEntity
    {
        public Guid SupplementaryResourceId { get; set; }
        public virtual SupplementaryResource SupplementaryResources { get; set; }

        public Guid EducationId  { get; set; }
        public virtual Education Educations { get; set; }

        public Guid SubjectId { get; set; }
        public virtual Subject Subjects { get; set; }


    }
}
