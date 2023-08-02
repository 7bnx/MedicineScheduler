using AutoMapper;
using MedicineScheduler.Common;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class LocationService : ILocationService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  public OperationResult<List<LocationDTO>> Get()
  {
    return new(_mapper.ProjectTo<LocationDTO>(_unitOfWork.Locations.GetAll()).ToList());
  }
}
