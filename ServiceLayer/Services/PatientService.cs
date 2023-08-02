using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class PatientService : IPatientService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  public OperationResult<List<PatientDTO>> Get()
  {
    return new(_mapper.ProjectTo<PatientDTO>(_unitOfWork.Patients.GetAll()).ToList());
  }
}
