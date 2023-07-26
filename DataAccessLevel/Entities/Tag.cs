namespace MedicineScheduler.DataAccessLayer.Entities;

public class Tag
{
  public string TagId { get; set; } = null!;
  public string TagName { get; set; } = string.Empty;
  public ICollection<Medicine>? Medicines { get; set; }
}
