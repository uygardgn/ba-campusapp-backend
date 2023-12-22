using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TrainingTypeRepository : EFBaseRepository<TrainingType>,ITrainingTypeRepository
    {
        public TrainingTypeRepository(BACampusAppDbContext context) : base(context) 
        {
        }
    }
}
