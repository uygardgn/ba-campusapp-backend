using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface ITrainingTypeRepository : IAsyncRepository, IAsyncInsertableRepository<TrainingType>, IAsyncFindableRepository<TrainingType>, IAsyncUpdateableRepository<TrainingType>, IAsyncDeleteableRepository<TrainingType>
    {
    }
}
