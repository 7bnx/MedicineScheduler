namespace MedicineScheduler.ServiceLayer.DTO;
public class TagDTO
{
  public int TagId { get; set; }
  public string Value { get; set; } = null!;
  public int MedicineId { get; set; }
}
