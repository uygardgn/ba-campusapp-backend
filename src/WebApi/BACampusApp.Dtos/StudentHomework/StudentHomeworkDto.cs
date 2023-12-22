using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkDto
    {
        public Guid Id { get; set; }
        public Guid HomeWorkId { get; set; }
        public Guid StudentId { get; set; }
        public string Title { get; set; }
        public string ReferansFile { get; set; }
        public string? AttachedFile { get; set; }
        public DateTime? SubmitDate { get; set; }//Teslim tarihi
        public double? Point { get; set; }//Ödev puanı
        public string? Feedback { get; set; }
        public string? ClassroomName { get; set; }
        public Guid? ClassroomId { get; set; }
        public HomeworkState? HomeworkState { get; set; } = Entities.Enums.HomeworkState.Assigned;//Ödev Durumu ->atandı/teslim edildi/teslim edilmedi/geç teslim edildi
    }
}
