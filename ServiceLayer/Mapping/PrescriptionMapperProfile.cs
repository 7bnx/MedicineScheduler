using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;

public class PrescriptionMapperProfile : Profile
{
  public PrescriptionMapperProfile()
  {
    CreateMap<Prescription, PrescriptionDTO>()
      .ForMember(dst => dst.RemainStorageDays, opt => opt
        .MapFrom(src => (int)Math.Round(src.Medicine.Orders == null
                        || src.AmountMedication == default || src.MedicationsPerDay == default ? 0 :
                           src.Medicine.Orders
                          .Where(o => o.DosageFormId == src.DosageFormId)
                          .Sum(o => o.Remains.Quantity) / (src.AmountMedication * src.MedicationsPerDay))))
      .ForMember(dst => dst.RemainStorageQuantity, opt => opt
        .MapFrom(src => src.Medicine.Orders == null ? 0 : src.Medicine.Orders
           .Where(o => o.DosageFormId == src.DosageFormId)
           .Sum(o => o.Remains.Quantity)));
    CreateMap<IPrescriptionDTO, Prescription>()
      .ForMember(dst => dst.Medicine, opt => opt.Ignore())
      .ForMember(dst => dst.DosageForm, opt => opt.Ignore())
      .ForMember(dst => dst.Patient, opt => opt.Ignore());

    CreateMap<IPrescriptionDTO, PrescriptionDTO>();

  }
}