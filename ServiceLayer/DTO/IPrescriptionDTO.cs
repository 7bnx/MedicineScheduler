using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.ServiceLayer.DTO;
public interface IPrescriptionDTO
{
  int PrescriptionId { get; set; }
  int DosageFormId { get; set; }
  DosageType DosageFormType { get; set; }
  DosageUnit DosageFormUnit { get; set; }
  double DosageFormValue { get; set; }
  DateTime EndDate { get; set; }
  int MedicineId { get; set; }
  string MedicineTitle { get; set; }
  string? Note { get; set; }
  int PatientId { get; set; }
  DateTime PrescriptionDate { get; set; }
  double AmountMedication { get; set; }
  int MedicationsPerDay { get; set; }
  int RemainStorageDays { get; set; }
  double RemainStorageQuantity { get; set; }
  DateTime StartDate { get; set; }
}