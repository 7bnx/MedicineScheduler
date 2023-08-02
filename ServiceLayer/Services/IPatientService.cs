using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IPatientService
{
  OperationResult<List<PatientDTO>> Get();
}
