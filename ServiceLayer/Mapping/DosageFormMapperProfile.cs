using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class DosageFormMapperProfile : Profile
{
  public DosageFormMapperProfile()
  {
    CreateMap<DosageForm, DosageFormDTO>().ReverseMap();
  }
}
