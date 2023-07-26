using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;

public class OrderRemainsMapperProfile : Profile
{
  public OrderRemainsMapperProfile()
  {
    CreateMap<OrderRemains, ChangeOrderRemainsDTO>();
  }
}
