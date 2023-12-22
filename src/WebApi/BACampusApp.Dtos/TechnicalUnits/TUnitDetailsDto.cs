using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.TechnicalUnits
{
    public class TUnitDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
