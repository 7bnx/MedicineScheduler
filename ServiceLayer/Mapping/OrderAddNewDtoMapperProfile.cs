using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class OrderAddNewDTOMapperProfile : Profile
{
  public OrderAddNewDTOMapperProfile()
  {
    CreateMap<OrderAddDTO, Order>()
      .ForMember(dst => dst.OrderId, opt => opt.Ignore())
      .ForMember(dst => dst.Medicine, opt => opt.Ignore())
      .ForMember(dst => dst.DosageForm, opt => opt.Ignore())
      .ForMember(dst => dst.Location, opt => opt.MapFrom(src => new Location() { LocationId = src.LocationId}))
      .ForMember(dst => dst.PriceTotal, opt => opt.MapFrom(src => src.PricePerPackage * src.QuantityPackages))
      .ForMember(dst => dst.Remains, opt => opt.ConvertUsing(new OrderAddNewDTOToOrderRemainsConverter(), src => src));

  }

  class OrderAddNewDTOToOrderRemainsConverter : IValueConverter<OrderAddDTO, OrderRemains>
  {
    public OrderRemains Convert(OrderAddDTO sourceMember, ResolutionContext context)
    {
      return new()
      {
        LastCheck = sourceMember.OrderDate,
        Quantity = sourceMember.QuantityPackages * sourceMember.QuantityPerPackage
      };
    }
  }

}
