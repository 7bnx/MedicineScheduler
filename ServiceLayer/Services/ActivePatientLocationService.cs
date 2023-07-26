using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;

public class ActivePatientLocationService : IActivePatientLocationService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public ActivePatientLocationService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  public OperationResult<ActivePatientLocationDTO> Get()
  {
    var collection = _unitOfWork.ActivePatientLocation.GetAll()?.OrderBy(a => a.ActivePatientLocationId);
    if (collection == null)
      return new(null! as ActivePatientLocationDTO, "Null collection");
    return new(_mapper.ProjectTo<ActivePatientLocationDTO>(collection)?.LastOrDefault());
  }
  public OperationResult<List<PatientDTO>> GetPatients()
  {
    return new(_mapper.ProjectTo<PatientDTO>(_unitOfWork.Patients.GetAll()).ToList());
  }

  public OperationResult<List<LocationDTO>> GetLocations()
  {
    return new(_mapper.ProjectTo<LocationDTO>(_unitOfWork.Locations.GetAll()).ToList());
  }

  public OperationResult<ActivePatientLocationDTO> Set(ActivePatientLocationDTO dto)
  {

    var active = _mapper.Map<ActivePatientLocation>(dto);

    _unitOfWork.ActivePatientLocation.Add(active);
    var saveResult = _unitOfWork.Save();

    return new(saveResult, dto with { LocationId = active.LocationId, PatientId = active.PatientId });
  }
}
