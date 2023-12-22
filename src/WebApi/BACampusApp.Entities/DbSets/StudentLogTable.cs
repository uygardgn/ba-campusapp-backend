using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class StudentLogTable : BaseEntity
    {
        public Guid StudentId { get; set; }
        public string? Description { get; set; } // Öğrenci kendi durumu hakkında yorumu
        public virtual Student Student { get; set; } // Öğrenci Aktif mi değilmi buradan alıyor
        public StudentActionType StudentActionType { get; set; }
        
    }
}
