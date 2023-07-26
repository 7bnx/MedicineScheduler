using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface IOrderAddService
{
  OperationResult<int> Add(OrderAddDTO dto);
}