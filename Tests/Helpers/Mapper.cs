using AutoMapper;

namespace MedicineScheduler.Tests.Helpers;

public static class Mapper
{
  public static IMapper Instance
    => _mapper;
  private static readonly IMapper _mapper =
    new MapperConfiguration(cfg =>
      {
        cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        cfg.AllowNullDestinationValues = true;
        cfg.AllowNullCollections = true;
      }
    ).CreateMapper();
  static Mapper ()
  {
    _mapper.ConfigurationProvider.AssertConfigurationIsValid();
  }
}
