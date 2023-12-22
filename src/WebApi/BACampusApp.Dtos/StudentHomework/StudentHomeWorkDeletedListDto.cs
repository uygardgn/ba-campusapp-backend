using BACampusApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentHomeWorkDeletedListDto
    {
        public Guid Id { get; set; }
        public Guid HomeWorkId { get; set; }
        public Guid StudentId { get; set; }
        public string? AttachedFile { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double? Point { get; set; }
        public HomeworkState? HomeworkState { get; set; } = Entities.Enums.HomeworkState.Assigned;
    }
}
