using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BACampusApp.Dtos.HomeWork
{
    public class HomeWorkCreateDto
    {
        public string Title { get; set; }
        public string? Intructions { get; set; }
        public IFormFile? ReferanceFile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SubjectId { get; set; }
        public bool IsLateTurnedIn { get; set; } = true;
        public bool IsHasPoint { get; set; }=true;
        public string? Description { get; set; }
        public Guid? ClassroomId { get; set; }
    }
}
