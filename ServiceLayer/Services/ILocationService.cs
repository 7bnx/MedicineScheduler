using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface ILocationService
{
  OperationResult<List<LocationDTO>> Get();
}
