
using System.Security.AccessControl;

namespace BACampusApp.Dtos.SupplementaryResources
{
    public class SupplementaryResourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }        
    }
}