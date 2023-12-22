using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class TokenBlackList : AuditableEntity
    {
        public string Token { get; set; }
    }
}
