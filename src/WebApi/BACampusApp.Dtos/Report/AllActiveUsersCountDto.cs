using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Report
{
    public class AllActiveUsersCountDto
    {
        public int ActiveAdminsCount { get; set; }
        public int ActiveTrainerssCount { get; set; }
        public int ActiveStudentsCount { get; set; }
    }
}
