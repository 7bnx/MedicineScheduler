using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class DosageFormService : IDosageFormService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public DosageFormService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public OperationResult<IEnumerable<DosageFormDTO>> Get()
  {
    return new(_mapper.ProjectTo<DosageFormDTO>(_unitOfWork.DosagesForms.GetAll()).AsEnumerable());
  }

  public OperationResult<int> Add1(DosageFormDTO dto)
  {
    if (_unitOfWork.DosagesForms.GetAll()!
        .Where(d => d.Type == dto.Type && d.Value == dto.Value && d.Unit == dto.Unit)
        .Count() > 0)
      return new(0);
    var dosageForm = _mapper.Map<DosageForm>(dto);
    if (dosageForm is null)
      return new("Mapping error", new InvalidOperationException());
    _unitOfWork.DosagesForms.Add(dosageForm);
    return _unitOfWork.Save();
  }

  public OperationResult<DosageFormDTO> Add(DosageFormDTO dto)
  {
    var dosageForm = _mapper.Map<DosageForm>(dto);
    if (dosageForm is null)
      return new("Mapping error", new InvalidOperationException());
    _unitOfWork.DosagesForms.Add(dosageForm);
    var operationResult = _unitOfWork.Save();
    return new(operationResult, dto with { DosageFormId = dosageForm.DosageFormId });
  }

}
