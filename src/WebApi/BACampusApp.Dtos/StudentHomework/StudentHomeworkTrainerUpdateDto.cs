using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeworkTrainerUpdateDto
    {
        public Guid Id { get; set; }
        public Guid HomeWorkId { get; set; }
        public Guid StudentId { get; set; }
        public IFormFile? AttachedFile { get; set; }
        public bool IsFileChanged { get; set; }
        //public DateTime? SubmitDate { get; set; }//Teslim tarihi
        public double? Point { get; set; }//Ödev puanı
                                          //public HomeworkState? HomeworkState { get; set; } = Entities.Enums.HomeworkState.Assigned;//Ödev Durumu ->atandı/teslim edildi/teslim edilmedi/geç teslim edildi
        public string? Feedback { get; set; }
        public bool IsHardDelete { get; set; }
    }
}
