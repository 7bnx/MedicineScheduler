using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.Tests.Helpers;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Mapping.Tests;
[TestClass]
public class ActivePatientLocationTest
{
  [TestMethod]
  public void Entity_To_DTO_OK()
  {
    var mapper = Mapper.Instance;
    var entity = new ActivePatientLocation()
    {
      ActivePatientLocationId = 1,
      Location = new Location { LocationId = 1, Name = "Location", Orders = Array.Empty<Order>() },
      LocationId = 1,
      Patient = new Patient { Name = "Patient", PatientId = 1, Prescriptions = Array.Empty<Prescription>() },
      PatientId = 1,
      SetTime = DateTime.Now,
    };
    var dto = mapper.Map<ActivePatientLocationDTO>(entity);
    Assert.AreEqual(entity.Patient.Name, dto.PatientName);
    Assert.AreEqual(entity.Location.Name, dto.LocationName);
  }

  [TestMethod]
  public void DTO_ToEntity_LocationId_Is_Set_OK()
  {
    var mapper = Mapper.Instance;
    var dto = new ActivePatientLocationDTO()
    {
      LocationId = 1,
      LocationName = "Location",
      PatientId = 0,
      PatientName = "Patient",
    };
    var entity = mapper.Map<ActivePatientLocation>(dto);
    Assert.AreEqual(dto.PatientName, entity.Patient.Name);
    Assert.AreEqual(dto.PatientId, entity.PatientId);
    Assert.AreEqual(null, entity.Location);
    Assert.AreEqual(dto.LocationId, entity.LocationId);
  } 
   
  [TestMethod]
  public void DTO_ToEntity_PatientId_Is_Set_OK()
  {
    var mapper = Mapper.Instance;
    var dto = new ActivePatientLocationDTO()
    {
      LocationId = 0,
      LocationName = "Location",
      PatientId = 1,
      PatientName = "Patient"
    };
    var entity = mapper.Map<ActivePatientLocation>(dto);
    Assert.AreEqual(null, entity.Patient);
    Assert.AreEqual(dto.PatientId, entity.PatientId);
    Assert.AreEqual(dto.LocationName, entity.Location.Name);
    Assert.AreEqual(dto.LocationId, entity.LocationId);
  }
  
  [TestMethod]
  public void DTO_ToEntity_LocationId_And_PatientId_Is_Set_OK()
  {
    var mapper = Mapper.Instance;
    var dto = new ActivePatientLocationDTO()
    {
      LocationId = 1,
      LocationName = "Location",
      PatientId = 1,
      PatientName = "Patient"
    };
    var entity = mapper.Map<ActivePatientLocation>(dto);
    Assert.AreEqual(null, entity.Patient);
    Assert.AreEqual(dto.PatientId, entity.PatientId);
    Assert.AreEqual(null, entity.Location);
    Assert.AreEqual(dto.LocationId, entity.LocationId);
  }

  [TestMethod]
  public void DTO_ToEntity_LocationId_And_PatientId_Not_Set_OK()
  {
    var mapper = Mapper.Instance;
    var dto = new ActivePatientLocationDTO()
    {
      LocationId = 0,
      LocationName = "Location",
      PatientId = 0,
      PatientName = "Patient"
    };
    var entity = mapper.Map<ActivePatientLocation>(dto);
    Assert.AreEqual(dto.PatientName, entity.Patient.Name);
    Assert.AreEqual(dto.PatientId, entity.PatientId);
    Assert.AreEqual(dto.LocationName, entity.Location.Name);
    Assert.AreEqual(dto.LocationId, entity.LocationId);
  }
}
