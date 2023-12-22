using BACampusApp.Core.Enums;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using Microsoft.AspNetCore.Http;
using System.Security.AccessControl;

namespace BACampusApp.Dtos.SupplementaryResources;

public class SupplementaryResourceCreateDto
{
    public string Name { get; set; }
    public List<Guid> Educations{ get; set; }
    public List<Guid> Subjects { get; set; }
    public IFormFile? FileURL { get; set; }
    public string? Link { get; set; }
    public int ResourceType { get; set; }
    public List<Guid> Tags { get; set; }
    public FileType FileType { get; set; } = FileType.Other;
    public ResourcesTypeStatus? ResourcesTypeStatus { get; set; }
}

