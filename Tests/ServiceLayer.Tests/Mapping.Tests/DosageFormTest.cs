using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MedicineScheduler.Tests.ServiceLayer.Tests.Mapping.Tests;

[TestClass]
public class DosageFormTest
{
  [TestMethod]
  public void Entity_To_DTO_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    DosageForm entity = new()
    {
      DosageFormId = 1,
      Type = Common.Enums.DosageType.Injection,
      Unit = Common.Enums.DosageUnit.Ml,
      Value = 10
    };

    _ = mapper.Map<DosageFormDTO>(entity);
  }
  
  [TestMethod]
  public void DTO_To_Entity_OK()
  {
    var mapper = Helpers.Mapper.Instance;
    DosageFormDTO dto = new()
    {
      DosageFormId = 1,
      Type = Common.Enums.DosageType.Injection,
      Unit = Common.Enums.DosageUnit.Ml,
      Value = 10
    };

    _ = mapper.Map<DosageForm>(dto);
  }
}
