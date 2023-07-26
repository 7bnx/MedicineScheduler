namespace MedicineScheduler.DataAccessLayer.Entities;

public class Patient
{
  public int PatientId { get; set; }
  public string Name { get; set; } = null!;
  public ICollection<Prescription>? Prescriptions { get; set; }
}
