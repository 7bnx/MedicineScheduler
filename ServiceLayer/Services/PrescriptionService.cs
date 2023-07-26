using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class PrescriptionService : IPrescriptionService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public PrescriptionService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public OperationResult<IEnumerable<PrescriptionDTO>> Get()
  {
    return new(_mapper.ProjectTo<PrescriptionDTO>(_unitOfWork.Prescriptions.GetAll()!).AsEnumerable());
  }

  public OperationResult<IEnumerable<PrescriptionDTO>> GetByPatientId(int id)
  {
    return new(_mapper.ProjectTo<PrescriptionDTO>(_unitOfWork.Prescriptions.GetAll()!.Where(p => p.PatientId == id)).AsEnumerable());
  }

  public OperationResult<PrescriptionDTO> Add(IPrescriptionDTO dto)
  {
    var prescription = _mapper.Map<Prescription>(dto);
    _unitOfWork.Prescriptions.Add(prescription);
    var saveResult = _unitOfWork.Save();

    return new(saveResult, _mapper.Map<PrescriptionDTO>(dto) with { PrescriptionId = prescription.PrescriptionId });
  }

  public OperationResult<IPrescriptionDTO> Remove(IPrescriptionDTO dto)
  {
    _unitOfWork.Prescriptions.RemoveById(dto.PrescriptionId);
    return new(_unitOfWork.Save(), dto);
  }

  public OperationResult<PrescriptionDTO> Update(IPrescriptionDTO dto)
  {
    var p = _unitOfWork.Prescriptions.FindById(dto.PrescriptionId);
    _mapper.Map(dto, p);
    return new(_unitOfWork.Save(), _mapper.Map<PrescriptionDTO>(dto) with { });
  }
}
