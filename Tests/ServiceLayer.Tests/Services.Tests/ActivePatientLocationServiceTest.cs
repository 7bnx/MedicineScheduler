using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.Services;
using Moq;
namespace MedicineScheduler.Tests.ServiceLayer.Tests.Services.Tests;

[TestClass]
public class ActivePatientLocationServiceTest
{
  [TestMethod]
  public void Get_ActivePatientLocation_OK()
  {
    List<ActivePatientLocation> testData = new()
    {
      new ActivePatientLocation(){ActivePatientLocationId = 77, PatientId = 11, Location = new(), Patient = new() },
      new ActivePatientLocation(){ActivePatientLocationId = 7, PatientId = 782, Location = new(), Patient = new()  },
      new ActivePatientLocation(){ActivePatientLocationId = 9, PatientId = 2, Location = new(), Patient = new()  },
      new ActivePatientLocation(){ActivePatientLocationId = 3, PatientId = 23, Location = new(), Patient = new()  },
    };
    var mapper = Helpers.Mapper.Instance;
    var uowStub = new Mock<IUnitOfWork>();
    var service = new ActivePatientLocationService(uowStub.Object, mapper);
    uowStub.Setup(s => s.ActivePatientLocation.GetAll()).Returns(testData.AsQueryable());
    var result = service.Get();
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsOk);
    Assert.IsNotNull(result.Value);
    Assert.AreEqual(testData.OrderBy(t => t.ActivePatientLocationId).Last().PatientId, result.Value.PatientId);
  }

  [TestMethod]
  public void Get_ActivePatientLocation_EmptyInput_Return_Null()
  {
    List<ActivePatientLocation> testData = new()
    {
    };
    var mapper = Helpers.Mapper.Instance;
    var uowStub = new Mock<IUnitOfWork>();
    var service = new ActivePatientLocationService(uowStub.Object, mapper);
    uowStub.Setup(s => s.ActivePatientLocation.GetAll()).Returns(testData.AsQueryable());
    var result = service.Get();
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsOk);
    Assert.IsNull(result.Value);
  }

  [TestMethod]
  public void Get_ActivePatientLocation_NullInput_Return_Null()
  {
    List<ActivePatientLocation> testData = null!;
    var mapper = Helpers.Mapper.Instance;
    var uowStub = new Mock<IUnitOfWork>();
    var service = new ActivePatientLocationService(uowStub.Object, mapper);
    uowStub.Setup(s => s.ActivePatientLocation.GetAll()).Returns(testData as IQueryable<ActivePatientLocation>);
    var result = service.Get();
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsOk);
    Assert.IsNull(result.Value);
  }
}
