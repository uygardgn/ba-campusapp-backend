using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class TrainerLogTableRepository : EFBaseRepository<TrainerLogTable>,ITrainerLogTableRepository
    {
        public TrainerLogTableRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
