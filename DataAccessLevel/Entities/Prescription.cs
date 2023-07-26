namespace MedicineScheduler.DataAccessLayer.Entities;

public class Prescription
{
  public int PrescriptionId { get; set; }
  public string? Note { get; set; }
  public int MedicineId { get; set; }
  public Medicine Medicine { get; set; } = null!;
  public int DosageFormId { get; set; }
  public DosageForm DosageForm { get; set; } = null!;
  public int PatientId { get; set; }
  public Patient Patient { get; set; } = null!;
  public double AmountMedication { get; set; }
  public int MedicationsPerDay { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public DateTime PrescriptionDate { get; set; }
}
