namespace MedicineScheduler.ServiceLayer.DTO;

public record ChangeOrderRemainsDTO
{
  public int OrderId { get; set; }
  public double Quantity { get; set; }
  public DateTime LastCheck { get; set; }
}
