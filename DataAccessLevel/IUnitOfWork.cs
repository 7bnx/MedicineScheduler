using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.DataAccessLayer.Repository;
using MedicineScheduler.Common;

namespace MedicineScheduler.DataAccessLayer;

public interface IUnitOfWork : IDisposable
{
  IRepository<DosageForm> DosagesForms { get; }
  IRepository<Location> Locations { get; }
  IRepository<ActivePatientLocation> ActivePatientLocation { get; }
  IRepository<Medicine> Medicines { get; }
  IRepository<Order> Orders { get; }
  IRepository<OrderRemains> OrdersRemains { get; }
  IRepository<Patient> Patients { get; }
  IRepository<Prescription> Prescriptions { get; }
  IRepository<Tag> Tags { get; }
  OperationResult<int> Save();
  Task<OperationResult<int>> SaveAsync();
  Task<OperationResult<int>> SaveAsync(CancellationToken token);
}
