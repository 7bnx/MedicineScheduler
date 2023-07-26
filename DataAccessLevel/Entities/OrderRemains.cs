namespace MedicineScheduler.DataAccessLayer.Entities;

public class OrderRemains
{
  public int OrderId { get; set; }
  public Order Order { get; set; } = null!;
  public double Quantity { get; set; }
  public DateTime LastCheck { get; set; }
}
