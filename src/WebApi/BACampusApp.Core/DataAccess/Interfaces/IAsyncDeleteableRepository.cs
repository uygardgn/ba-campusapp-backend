namespace BACampusApp.Core.DataAccess.Interfaces;

public interface IAsyncDeleteableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
{
    Task DeleteAsync(TEntity entity);
}
