namespace MedicineScheduler.ServiceLayer.DTO;

public record OrderAddDTO
{
  public int MedicineId { get; set; }
  public int DosageFormId { get; set; }
  public int QuantityPerPackage { get; set; }
  public int QuantityPackages { get; set; }
  public DateTime ProduceDate { get; set; }
  public DateTime ExpirationDate { get; set; }
  public DateTime OrderDate { get; set; }
  public decimal PricePerPackage { get; set; }
  public int LocationId { get; set; }
}
