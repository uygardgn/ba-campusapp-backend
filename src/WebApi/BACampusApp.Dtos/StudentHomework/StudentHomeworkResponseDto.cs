using BACampusApp.Core.Enums;
using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkResponseDto
    {
        public Guid HomeWorkId { get; set; }
        public Guid StudentId { get; set; }
        public IFormFile? ReferanceFile { get; set; }//dosya yolu ancak bu senaryoda birden fazla eklenemiyor.
        public DateTime? SubmitDate { get; set; }//Teslim tarihi
        public HomeworkState HomeworkState { get; set; }//Ödev Durumu ->atandı/teslim edildi/teslim edilmedi
    }
}
