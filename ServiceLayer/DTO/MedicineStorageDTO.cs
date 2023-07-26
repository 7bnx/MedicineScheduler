using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.ServiceLayer.DTO;

public record MedicineStorageDTO
{
  public int MedicineId { get; set; }
  public string Title { get; set; } = null!;
  public int DosageFormId { get; set; }
  public DosageType MedicineType { get; set; }
  public double DosageValue { get; set; }
  public DosageUnit DosageUnit { get; set; }
  public double RemainQuantity { get; set; }
}
