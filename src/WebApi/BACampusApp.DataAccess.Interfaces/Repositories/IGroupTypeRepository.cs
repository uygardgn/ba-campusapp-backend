using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IGroupTypeRepository : IAsyncRepository, IAsyncInsertableRepository<GroupType>, IAsyncFindableRepository<GroupType>, IAsyncUpdateableRepository<GroupType>, IAsyncDeleteableRepository<GroupType>
    {
    }
}
