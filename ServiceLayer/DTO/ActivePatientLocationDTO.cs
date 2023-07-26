namespace MedicineScheduler.ServiceLayer.DTO;
public record ActivePatientLocationDTO : IActivePatientLocationDTO
{
  public int PatientId { get; set; }
  public int LocationId { get; set; }
  public string PatientName { get; set; } = null!;
  public string LocationName { get; set; } = null!;
}
