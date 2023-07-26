using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.ServiceLayer.DTO;

public record PrescriptionDTO : IPrescriptionDTO
{
  public int PrescriptionId { get; set; }
  public string? Note { get; set; }
  public int MedicineId { get; set; }
  public string MedicineTitle { get; set; } = null!;
  public int DosageFormId { get; set; }
  public DosageType DosageFormType { get; set; }
  public double DosageFormValue { get; set; }
  public DosageUnit DosageFormUnit { get; set; }
  public double AmountMedication { get; set; }
  public int MedicationsPerDay { get; set; }
  public double RemainStorageQuantity { get; set; }
  public int RemainStorageDays { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public DateTime PrescriptionDate { get; set; }
  public int PatientId { get; set; }
}
