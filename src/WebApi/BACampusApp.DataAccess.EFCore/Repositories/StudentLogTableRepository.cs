using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class StudentLogTableRepository : EFBaseRepository<StudentLogTable>, IStudentLogTableRepository
    {
        public StudentLogTableRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}