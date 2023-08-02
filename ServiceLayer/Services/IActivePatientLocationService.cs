using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IActivePatientLocationService
{
  OperationResult<ActivePatientLocationDTO> Get();
  OperationResult<ActivePatientLocationDTO> Set(ActivePatientLocationDTO dto);
}