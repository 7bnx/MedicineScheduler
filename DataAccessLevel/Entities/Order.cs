namespace MedicineScheduler.DataAccessLayer.Entities;

public class Order
{
  public int OrderId { get; set; }
  public int MedicineId { get; set; }
  public Medicine Medicine { get; set; } = null!;
  public int DosageFormId { get; set; }
  public DosageForm? DosageForm { get; set; }
  public int QuantityPerPackage { get; set; }
  public int QuantityPackages { get; set; }
  public DateTime ProduceDate { get; set; }
  public DateTime ExpirationDate { get; set;}
  public DateTime OrderDate { get; set; }
  public decimal PricePerPackage { get; set; }
  public decimal PriceTotal { get; private set; }
  public int LocationId { get; set; }
  public Location Location { get; set; } = null!;
  public OrderRemains Remains { get; set; } = null!;
}
