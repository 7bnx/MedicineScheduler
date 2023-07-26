using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.DataAccessLayer.Entities;
public class DosageForm
{
  public int DosageFormId { get; set; }
  public DosageType Type { get; set; }
  public double Value { get; set; }
  public DosageUnit Unit { get; set; }
}