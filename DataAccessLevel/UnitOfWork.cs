using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using MedicineScheduler.Common;

namespace MedicineScheduler.DataAccessLayer;

public sealed class UnitOfWork : IUnitOfWork
{
  public IRepository<DosageForm> DosagesForms { get; init; }
  public IRepository<Location> Locations { get; init; }
  public IRepository<ActivePatientLocation> ActivePatientLocation { get; init; }
  public IRepository<Medicine> Medicines { get; init; }
  public IRepository<Order> Orders { get; init; }
  public IRepository<OrderRemains> OrdersRemains { get; init; }
  public IRepository<Patient> Patients { get; init; }
  public IRepository<Prescription> Prescriptions { get; init; }
  public IRepository<Tag> Tags { get; init; }

  private bool _disposed;
  private readonly DbContext _context;
  public UnitOfWork(EFContext context)
  {
    _context = context;
    DosagesForms = new RepositoryBase<DosageForm>(_context);
    Locations = new RepositoryBase<Location>(_context);
    ActivePatientLocation = new RepositoryBase<ActivePatientLocation>(_context);
    Medicines = new RepositoryBase<Medicine>(_context);
    Orders = new RepositoryBase<Order>(_context);
    OrdersRemains = new RepositoryBase<OrderRemains>(_context);
    Patients = new RepositoryBase<Patient>(_context);
    Prescriptions = new RepositoryBase<Prescription>(_context);
    Tags = new RepositoryBase<Tag>(_context);
  }

  public OperationResult<int> Save()
    => SaveHelper(() => Task.FromResult(_context.SaveChanges())).Result;

  public async Task<OperationResult<int>> SaveAsync()
    => await SaveHelper(async () => await _context.SaveChangesAsync());
  public async Task<OperationResult<int>> SaveAsync(CancellationToken token)
    => await SaveHelper(async () => await _context.SaveChangesAsync(token));

  private async Task<OperationResult<int>> SaveHelper(Func<Task<int>> saveAction)
  {
    var resultValidation = _context.ExecuteValidation();
    if (resultValidation.Any()) return new(resultValidation, "Error validating");

    _context.ChangeTracker.AutoDetectChangesEnabled = false;
    try
    {
      return new(await saveAction().ConfigureAwait(false));
    } catch (Exception ex)
    {
      return new("Error saving changes", ex);
    } finally
    {
      _context.ChangeTracker.AutoDetectChangesEnabled = true;
    }
  }

  public void Dispose()
    => Dispose(true);
  private void Dispose(bool disposing)
  {
    if (!_disposed)
    {
      if (disposing) { }
      _context?.Dispose();
    }
    _disposed = true;
  }

  ~UnitOfWork()
    => Dispose(false);
}
