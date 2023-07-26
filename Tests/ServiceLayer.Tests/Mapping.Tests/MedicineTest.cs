using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Mapping.Tests;

[TestClass]
public class MedicineTest
{
  [TestMethod]
  public void Entity_To_DTO_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    Medicine entity = new()
    {
      Description = "description",
      MedicineId = 1,
      Orders = null,
      Prescriptions = null,
      Title = "title",
      Tags = new[] {new Tag() { TagId = "Tag1" }, new Tag() { TagId = "Tag2"} }
    };

    var dto = mapper.Map<MedicineDTO>(entity);
    Assert.IsNotNull(dto.Tags);
    foreach (var t in entity.Tags)
      Assert.IsTrue(dto.Tags.Any(s => s == t.TagId));
  }
  
  [TestMethod]
  public void DTO_To_Entity_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    MedicineDTO dto = new()
    {
      Description = "description",
      MedicineId = 1,
      Title = "title"
    };

    _ = mapper.Map<Medicine>(dto);
  }
}
