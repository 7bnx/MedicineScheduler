using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;

public class ChangeOrderRemainsService : IChangeOrderRemainsService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public ChangeOrderRemainsService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public OperationResult<ChangeOrderRemainsDTO> Get(int id)
    => HelperGet(id, (id) => Task.FromResult(_unitOfWork.OrdersRemains.FindById(id))).Result;

  public async Task<OperationResult<ChangeOrderRemainsDTO>> GetAsync(int id)
    => await HelperGet(id, id => _unitOfWork.OrdersRemains.FindByIdAsync(id));

  public async Task<OperationResult<ChangeOrderRemainsDTO>> GetAsync(int id, CancellationToken token)
    => await HelperGet(id, id => _unitOfWork.OrdersRemains.FindByIdAsync(id, token));

  public OperationResult<int> Update(ChangeOrderRemainsDTO remainsDto)
    => HelperUpdate
    (
      remainsDto,
      id => Task.FromResult(_unitOfWork.OrdersRemains.FindById(id)),
      () => Task.FromResult(_unitOfWork.Save())
    ).Result;

  public async Task<OperationResult<int>> UpdateAsync(ChangeOrderRemainsDTO remainsDto)
    => await HelperUpdate
    (
      remainsDto,
      id => _unitOfWork.OrdersRemains.FindByIdAsync(id),
      () => _unitOfWork.SaveAsync()
    );

  public async Task<OperationResult<int>> UpdateAsync(ChangeOrderRemainsDTO remainsDto, CancellationToken token)
    => await HelperUpdate
    (
      remainsDto,
      id => _unitOfWork.OrdersRemains.FindByIdAsync(id, token),
      () => _unitOfWork.SaveAsync(token)
    );

  private async Task<OperationResult<ChangeOrderRemainsDTO>> HelperGet(int id, Func<int, Task<OrderRemains?>> get)
  {
    var remainsOriginal = await get(id).ConfigureAwait(false);
    if (remainsOriginal is null)
      return new("Remains not found", new ArgumentException($"Id({id}) not exists", nameof(id)));
    var remainsDto = _mapper.Map<ChangeOrderRemainsDTO>(remainsOriginal);
    if (remainsDto is null)
      return new("Mapping error", new InvalidOperationException($"OrderRemains not mapped id: {id}"));
    return new(remainsDto);
  }

  private static async Task<OperationResult<int>> HelperUpdate
  (
    ChangeOrderRemainsDTO remainsDto,
    Func<int, Task<OrderRemains?>> findById,
    Func<Task<OperationResult<int>>> save
  )
  {
    if (remainsDto.Quantity < 0)
      return new("Quantity should be grater or equals to zero",
             new ArgumentException($"Wrong {nameof(remainsDto.Quantity)} value {remainsDto.Quantity}", nameof(remainsDto)));
    var id = remainsDto.OrderId;
    var remainsOriginal = await findById(id).ConfigureAwait(false);
    if (remainsOriginal is null)
      return new("Remains not found", new ArgumentException($"Id({id}) not exists", nameof(remainsDto)));
    remainsOriginal.Quantity = remainsDto.Quantity;
    remainsOriginal.LastCheck = remainsDto.LastCheck;

    return await save().ConfigureAwait(false);
  }
}
