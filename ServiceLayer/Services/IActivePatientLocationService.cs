using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IActivePatientLocationService
{
  OperationResult<ActivePatientLocationDTO> Get();
  OperationResult<List<LocationDTO>> GetLocations();
  OperationResult<List<PatientDTO>> GetPatients();
  OperationResult<ActivePatientLocationDTO> Set(ActivePatientLocationDTO dto);
}