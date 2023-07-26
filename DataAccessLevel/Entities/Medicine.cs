namespace MedicineScheduler.DataAccessLayer.Entities;

public class Medicine
{
  public int MedicineId { get; set; }
  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public ICollection<Prescription>? Prescriptions { get; set; }
  public ICollection<Order>? Orders { get; set; }
  public ICollection<Tag>? Tags { get; set; }
}
