using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.Services;
using Moq;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Services.Tests;

[TestClass]
public class PatientServiceTest
{
  [TestMethod]
  public void Get_Patients_OK()
  {
    var uowStub = new Mock<IUnitOfWork>();
    var mapper = Helpers.Mapper.Instance;
    var service = new PatientService(uowStub.Object, mapper);
    uowStub.Setup(s => s.Patients.GetAll()).Returns(new List<Patient>().AsQueryable());
    var result = service.Get();
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsOk);
    Assert.IsNotNull(result.Value);
  }
}
