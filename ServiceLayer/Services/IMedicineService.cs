using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IMedicineService
{
  OperationResult<MedicineDTO> Add(MedicineDTO dto);
  OperationResult<IEnumerable<MedicineDTO>> Get();
}