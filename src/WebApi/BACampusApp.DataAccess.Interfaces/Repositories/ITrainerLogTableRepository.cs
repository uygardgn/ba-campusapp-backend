using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITrainerLogTableRepository : IAsyncRepository, IAsyncInsertableRepository<TrainerLogTable>, IAsyncFindableRepository<TrainerLogTable>, IAsyncQueryableRepository<TrainerLogTable>
    {
    }
}
