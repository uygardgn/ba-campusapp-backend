using BACampusApp.Core.Enums;

namespace BACampusApp.Core.Entities.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    Status Status { get; set; }
}
