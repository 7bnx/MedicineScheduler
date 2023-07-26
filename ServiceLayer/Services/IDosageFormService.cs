using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IDosageFormService
{
  OperationResult<DosageFormDTO> Add(DosageFormDTO dto);
  OperationResult<int> Add1(DosageFormDTO dto);
  OperationResult<IEnumerable<DosageFormDTO>> Get();
}