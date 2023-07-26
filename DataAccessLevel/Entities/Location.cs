namespace MedicineScheduler.DataAccessLayer.Entities;
public class Location
{
  public int LocationId { get; set; }
  public string Name { get; set; } = null!;
  public ICollection<Order>? Orders { get; set; }
}
