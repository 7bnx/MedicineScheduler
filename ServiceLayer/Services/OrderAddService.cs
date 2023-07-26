using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class OrderAddService : IOrderAddService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public OrderAddService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public OperationResult<int> Add(OrderAddDTO dto)
  {
    var b = _unitOfWork.Medicines.GetAll()!
            .Join(_unitOfWork.DosagesForms.GetAll()!,
                m => m.MedicineId, d => dto.MedicineId, (m, d) => new { m, d })
                .Where(x => x.d.DosageFormId == dto.DosageFormId)
            .Count() > 0;
    var order = _mapper.Map<Order>(dto);
    if (order is null)
      return new("Mapping error", new InvalidOperationException());
    _unitOfWork.Orders.Add(order);
    return _unitOfWork.Save();
  }
}
