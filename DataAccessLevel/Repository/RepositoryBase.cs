using Microsoft.EntityFrameworkCore;

namespace MedicineScheduler.DataAccessLayer.Repository;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
  protected readonly DbContext _context;
  public RepositoryBase(DbContext context)
    => _context = context;

  public void Add(TEntity entity)
    => _context.Add(entity);

  public void AddRange(IEnumerable<TEntity> entities)
    => _context.AddRange(entities);

  public void Remove(TEntity entity)
    => _context.Set<TEntity>().Remove(entity);
  public void RemoveById(int id)
  {
    var entity = _context.Set<TEntity>().Find(id);
    if(entity is not null)
      _context.Remove(entity);
  }

  public void RemoveRange(IEnumerable<TEntity> entities)
    => _context.RemoveRange(entities);

  public TEntity? Find(TEntity entity)
    => _context.Find<TEntity>(entity);
  public async Task<TEntity?> FindAsync(TEntity entity)
    => await _context.FindAsync<TEntity>(entity);

  public IQueryable<TEntity>? GetAll()
    => _context.Set<TEntity>().AsNoTracking();

  public async Task<IList<TEntity>?> GetAllAsync()
    => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

  public void UpdateRange(IEnumerable<TEntity> entities)
      => _context.UpdateRange(entities);
  public void Update(TEntity entity)
    => _context.Set<TEntity>().Update(entity);

  public TEntity? FindById(int id)
    => _context.Set<TEntity>().Find(id);

  public async Task<TEntity?> FindByIdAsync(int id)
    => await _context.Set<TEntity>().FindAsync(id);

  public async Task<TEntity?> FindByIdAsync(int id, CancellationToken token)
    => await _context.Set<TEntity>().FindAsync(new object[] { id }, token);
}
