namespace MedicineScheduler.DataAccessLayer.Repository;

public interface IRepository<TEntity> where TEntity : class
{
  void Add(TEntity entity);
  void AddRange(IEnumerable<TEntity> entities);
  IQueryable<TEntity>? GetAll();
  Task<IList<TEntity>?> GetAllAsync();
  TEntity? Find(TEntity entity);
  TEntity? FindById(int id);
  Task<TEntity?> FindByIdAsync(int id);
  Task<TEntity?> FindByIdAsync(int id, CancellationToken token);
  Task<TEntity?> FindAsync(TEntity entity);
  void Update(TEntity entity);
  void UpdateRange(IEnumerable<TEntity> entities);
  void Remove(TEntity entity);
  void RemoveById(int id);
  void RemoveRange(IEnumerable<TEntity> entities);
}
