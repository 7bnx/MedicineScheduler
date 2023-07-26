using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class PatientMapperProfile : Profile
{
  public PatientMapperProfile()
  {
    CreateMap<Patient, PatientDTO>()
      .ForMember(dst => dst.PatientName, opt => opt.MapFrom(src => src.Name));
  }
}
