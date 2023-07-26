using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public interface ITagAddService
{
  void Add(TagDTO dto);
}