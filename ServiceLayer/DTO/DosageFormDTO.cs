using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.ServiceLayer.DTO;
public record DosageFormDTO
{
  public int DosageFormId { get; set; }
  public DosageType Type { get; set; }
  public double Value { get; set; }
  public DosageUnit Unit { get; set; }
}
