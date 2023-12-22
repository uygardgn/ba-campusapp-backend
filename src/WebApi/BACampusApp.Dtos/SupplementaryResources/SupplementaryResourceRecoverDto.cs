using BACampusApp.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceRecoverDto
    {
        public Guid Id { get; set; }
        public List<Guid> Subjects { get; set; }
        public List<Guid> Educations { get; set; }
        public string Name { get; set; }
        public List<Guid> Tags { get; set; }
        public bool IsHardDelete { get; set; }
        //public int ResourceType { get; set; }
        //public ResourcesTypeStatus? ResourcesTypeStatus { get; set; }
    }
}
