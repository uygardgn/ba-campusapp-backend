using BACampusApp.Core.Enums;
using BACampusApp.Entities.DbSets;
using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentHomework
{
    public class StudentListByHomeworkIdDto
    {
        public Guid StudentHomeworkId { get; set; }
        public Guid HomeWorkId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AttachedFile { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double? Point { get; set; }
        public string? Feedback { get; set; }
        public HomeworkState HomeworkState { get; set; }
        public Guid StudentId { get; set; }
    }
}
