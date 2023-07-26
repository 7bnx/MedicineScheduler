using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Mapping.Tests;

[TestClass]
public class PatientTest
{
  [TestMethod]
  public void Entity_To_Dto_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    Patient entity = new()
    {
      Name = "Patient",
      PatientId = 1,
      Prescriptions = null
    };
    var dto = mapper.Map<PatientDTO>(entity);
    Assert.AreEqual(entity.Name, dto.PatientName);
    Assert.AreEqual(entity.PatientId, dto.PatientId);
  }
}
