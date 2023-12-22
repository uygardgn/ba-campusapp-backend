using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class SupplementaryResourceEducationSubjectRepository : EFBaseRepository<SupplementaryResourceEducationSubject>, ISupplementaryResourceEducationSubjectRepository
    {
        public SupplementaryResourceEducationSubjectRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}

