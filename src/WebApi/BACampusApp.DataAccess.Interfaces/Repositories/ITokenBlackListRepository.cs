using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITokenBlackListRepository : IAsyncRepository, IAsyncInsertableRepository<TokenBlackList>, IAsyncFindableRepository<TokenBlackList>, IAsyncDeleteableRepository<TokenBlackList>
    {
    }
}
