namespace MedicineScheduler.DataAccessLayer.Entities;
public class ActivePatientLocation
{
  public int ActivePatientLocationId { get; set; }
  public int PatientId { get; set; }
  public Patient Patient { get; set; } = null!;
  public int LocationId { get; set; }
  public Location Location { get; set; } = null!;
  public DateTime SetTime { get; set; }
}
