using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class MedicineService : IMedicineService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public MedicineService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public OperationResult<IEnumerable<MedicineDTO>> Get()
  {
    return new(_mapper.ProjectTo<MedicineDTO>(_unitOfWork.Medicines.GetAll()!));
  }

  public OperationResult<MedicineDTO> Add(MedicineDTO dto)
  {
    var medicine = _mapper.Map<Medicine>(dto);
    _unitOfWork.Medicines.Add(medicine);
    var operationResult = _unitOfWork.Save();
    return new(operationResult, dto with { MedicineId = medicine.MedicineId });
  }

}
