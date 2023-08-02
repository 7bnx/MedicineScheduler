using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.Services;
using Moq;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Services.Tests;

[TestClass]
public class LocationServiceTest
{
  [TestMethod]
  public void Get_Locations_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    var uowStub = new Mock<IUnitOfWork>();
    var service = new LocationService(uowStub.Object, mapper);
    uowStub.Setup(s => s.Locations.GetAll()).Returns(new List<Location>().AsQueryable());
    var result = service.Get();
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsOk);
    Assert.IsNotNull(result.Value);
  }
}
