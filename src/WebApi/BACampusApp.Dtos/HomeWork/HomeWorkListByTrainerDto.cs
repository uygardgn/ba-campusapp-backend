using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.HomeWork
{
    public class HomeWorkListByTrainerDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Intructions { get; set; }
        public string? ReferansFile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SubjectId { get; set; }
        public bool IsLateTurnedIn { get; set; }
        public bool IsHasPoint { get; set; }
        public string? Description { get; set; }
        public Guid? ClassroomId { get; set; }
    }
}
