using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IChangeOrderRemainsService
{
  OperationResult<ChangeOrderRemainsDTO> Get(int id);
  Task<OperationResult<ChangeOrderRemainsDTO>> GetAsync(int id);
  Task<OperationResult<ChangeOrderRemainsDTO>> GetAsync(int id, CancellationToken token);
  OperationResult<int> Update(ChangeOrderRemainsDTO remainsDto);
  Task<OperationResult<int>> UpdateAsync(ChangeOrderRemainsDTO remainsDto);
  Task<OperationResult<int>> UpdateAsync(ChangeOrderRemainsDTO remainsDto, CancellationToken token);
}