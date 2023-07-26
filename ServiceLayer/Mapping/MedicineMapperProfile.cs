using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;

public class MedicineMapperProfile : Profile
{
  public MedicineMapperProfile()
  {
    CreateMap<Medicine, MedicineDTO>()
      .ForMember(dst => dst.Tags, o => o.MapFrom(z => z.Tags!.Select(t => t.TagId)));
    CreateMap<IQueryable<Medicine>, IQueryable<MedicineStorageDTO>>()
      .ConvertUsing<Converter>();
    CreateMap<MedicineDTO, Medicine>()
      .ForMember(dst => dst.Orders, o => o.Ignore())
      .ForMember(dst => dst.Prescriptions, o => o.Ignore())
      .ForMember(dst => dst.Tags, o => o.Ignore());
  }
}

file class Converter : ITypeConverter<IQueryable<Medicine>, IQueryable<MedicineStorageDTO>>
{
  public IQueryable<MedicineStorageDTO> Convert(IQueryable<Medicine> source, IQueryable<MedicineStorageDTO> destination, ResolutionContext context)
  {/*
    return source.SelectMany(md => md.DosageForms, (m, d) => new MedicineStorageDTO
    {
      MedicineId = m.MedicineId,
      Title = m.Title,
      MedicineType = d.Type,
      DosageFormId = d.DosageFormId,
      DosageUnit = d.Unit,
      DosageValue = d.Value,
      RemainQuantity = m.Orders!
                        .Where(o => o.DosageFormId == d.DosageFormId)
                        .Sum(o => o.Remains.Quantity)
    });*/
    return null;
  }
}
