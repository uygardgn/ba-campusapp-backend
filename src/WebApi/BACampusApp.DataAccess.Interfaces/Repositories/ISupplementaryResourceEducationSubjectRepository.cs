using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ISupplementaryResourceEducationSubjectRepository : IAsyncInsertableRepository<SupplementaryResourceEducationSubject>, IAsyncFindableRepository<SupplementaryResourceEducationSubject>, IAsyncRepository, IAsyncUpdateableRepository<SupplementaryResourceEducationSubject>, IAsyncDeleteableRepository<SupplementaryResourceEducationSubject>,
        IAsyncQueryableRepository<SupplementaryResourceEducationSubject>
    {

    }
}
