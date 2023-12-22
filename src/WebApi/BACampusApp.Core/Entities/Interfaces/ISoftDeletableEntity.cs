namespace BACampusApp.Core.Entities.Interfaces;

public interface ISoftDeletableEntity : ICreateableEntity, IEntity
{
    string? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}
