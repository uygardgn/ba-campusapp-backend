
using Microsoft.AspNetCore.Identity;

namespace BACampusApp.Core.DataAccess.EntityFramework;

public abstract class EFBaseRepository<TEntity> : IAsyncOrderableRepository<TEntity>, IAsyncFindableRepository<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncInsertableRepository<TEntity>, IAsyncUpdateableRepository<TEntity>, IAsyncDeleteableRepository<TEntity>, IAsyncRepository, IAsyncTransactionRepository, IRepository
    where TEntity : BaseEntity
{
    protected readonly IdentityDbContext<IdentityUser, IdentityRole, string> _context;
    protected readonly DbSet<TEntity> _table;

    protected EFBaseRepository(IdentityDbContext<IdentityUser, IdentityRole, string> context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _table.AddAsync(entity);

        return entry.Entity;
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _table.AddRangeAsync(entities);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
    }

    public void Delete(TEntity entity)
    {
        _table.Remove(entity);
    }

    public Task DeleteAsync(TEntity entity)
    {
        return Task.FromResult(_table.Remove(entity));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
    {
        return await GetAllActives(tracking).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return await GetAllActives(tracking).Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking);

        return orderDesc ? await values.OrderByDescending(orderby).ToListAsync() : await values.OrderBy(orderby).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking).Where(expression);

        return orderDesc ? await values.OrderByDescending(orderby).ToListAsync() : await values.OrderBy(orderby).ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        var values = GetAllActives(tracking);
        return await values.FirstOrDefaultAsync(expression);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
    {
        var values = GetAllActives(tracking);

        return values.FirstOrDefaultAsync(x => x.Id == id);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = await Task.FromResult(_table.Update(entity));
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public Task<IExecutionStrategy> CreateExecutionStrategy()
    {
        return Task.FromResult(_context.Database.CreateExecutionStrategy());
    }

    protected IQueryable<TEntity> GetAllActives(bool tracking = true)
    {
        var values = _table.Where(x => x.Status != Status.Deleted);

        return tracking ? values : values.AsNoTracking();
    }

    public async Task<IEnumerable<TEntity>> GetAllDeletedAsync(bool tracking = true)
    {
        return await GetAllIActives(tracking).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllDeletedAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return await GetAllIActives(tracking).Where(expression).ToListAsync();
    }


    protected IQueryable<TEntity> GetAllIActives(bool tracking = true)
    {
        var values = _table.Where(x => x.Status == Status.Deleted);

        return tracking ? values : values.AsNoTracking();
    }
}
