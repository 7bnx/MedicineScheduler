namespace MedicineScheduler.ServiceLayer.DTO;
public record MedicineDTO
{
  public int MedicineId { get; set; }
  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public string[]? Tags { get; set; }
}
