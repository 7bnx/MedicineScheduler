using AutoMapper;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.DataAccessLayer.Mapping;
public class TagMapperProfile : Profile
{
  public TagMapperProfile()
  {
    //CreateMap<Tag, TagDto>()
      //.ForMember(dst => dst.MedicinesID, opt => opt.MapFrom(src => src.Medicines!.Select(m => m.MedicineId)));

  }
}
