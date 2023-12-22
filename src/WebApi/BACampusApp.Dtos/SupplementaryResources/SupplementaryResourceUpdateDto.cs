
using BACampusApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceUpdateDto
    {
        public Guid Id { get; set; }
        public List<Guid> Subjects { get; set; }
        public List<Guid> Educations { get; set; }
        public string Name { get; set; }
        public IFormFile? FileURL { get; set; }
        public List<Guid> Tags { get; set; }
        public bool IsHardDelete { get; set; }
        public string? Link { get; set; }
        public int ResourceType { get; set; }
        public FileType FileType { get; set; } = FileType.Other;
        public ResourcesTypeStatus? ResourcesTypeStatus { get; set; }
    }
}
