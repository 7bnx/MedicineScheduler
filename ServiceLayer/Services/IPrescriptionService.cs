using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IPrescriptionService
{
  OperationResult<PrescriptionDTO> Add(IPrescriptionDTO dto);
  OperationResult<IEnumerable<PrescriptionDTO>> Get();
  OperationResult<IEnumerable<PrescriptionDTO>> GetByPatientId(int id);
  OperationResult<IPrescriptionDTO> Remove(IPrescriptionDTO dto);
  OperationResult<PrescriptionDTO> Update(IPrescriptionDTO dto);
}