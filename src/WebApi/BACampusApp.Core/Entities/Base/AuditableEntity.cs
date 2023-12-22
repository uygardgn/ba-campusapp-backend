using BACampusApp.Core.Entities.Interfaces;

namespace BACampusApp.Core.Entities.Base;

public abstract class AuditableEntity : BaseEntity, ISoftDeletableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}
