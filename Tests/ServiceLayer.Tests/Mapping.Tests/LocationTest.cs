using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MedicineScheduler.Tests.ServiceLayer.Tests.Mapping.Tests;

[TestClass]
public class LocationTest
{
  [TestMethod]
  public void Entity_To_DTO_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    Location entity = new()
    {
      LocationId = 1,
      Name = "Location",
      Orders = null
    };

    var dto = mapper.Map<LocationDTO>(entity);
    Assert.AreEqual(entity.Name, dto.LocationName);
  }
}
