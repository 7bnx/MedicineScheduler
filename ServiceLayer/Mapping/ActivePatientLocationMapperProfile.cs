using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class ActivePatientLocationMapperProfile : Profile
{
  public ActivePatientLocationMapperProfile()
  {
    CreateMap<ActivePatientLocation, ActivePatientLocationDTO>();
    CreateMap<ActivePatientLocationDTO, ActivePatientLocation>()
      .ForMember(dst => dst.Patient, opt => opt.MapFrom(src => src.PatientId != 0 ? null! : new Patient() { Name = src.PatientName, PatientId = src.PatientId}))
      .ForMember(dst => dst.Location, opt => opt.MapFrom(src => src.LocationId != 0 ? null! : new Location() { Name = src.LocationName, LocationId = src.LocationId}))
      .ForMember(dst => dst.ActivePatientLocationId, opt => opt.Ignore())
      .ForMember(dst => dst.SetTime, opt => opt.MapFrom(src => DateTime.Now));
  }
}
