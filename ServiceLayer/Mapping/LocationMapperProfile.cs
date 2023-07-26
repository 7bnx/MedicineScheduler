using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class LocationMapperProfile : Profile
{
  public LocationMapperProfile()
  {
    CreateMap<Location, LocationDTO>()
      .ForMember(dst => dst.LocationName, opt => opt.MapFrom(src => src.Name));
  }
}
