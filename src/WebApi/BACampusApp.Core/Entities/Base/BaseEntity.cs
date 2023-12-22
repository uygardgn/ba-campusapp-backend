using BACampusApp.Core.Entities.Interfaces;
using BACampusApp.Core.Enums;

namespace BACampusApp.Core.Entities.Base;

public abstract class BaseEntity : IEntity, ICreateableEntity, IUpdateableEntity
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
}
