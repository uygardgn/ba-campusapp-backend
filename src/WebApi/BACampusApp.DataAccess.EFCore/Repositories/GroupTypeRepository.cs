using BACampusApp.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class GroupTypeRepository : EFBaseRepository<GroupType>,IGroupTypeRepository
    {
        public GroupTypeRepository(BACampusAppDbContext context) : base(context)
        {
        }
    }
}