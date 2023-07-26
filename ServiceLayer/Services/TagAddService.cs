using AutoMapper;
using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.Services;
public class TagAddService : ITagAddService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public TagAddService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public void Add(TagDTO dto)
  {
    var tag = _unitOfWork.Tags.GetAll()?.Where(t => t.TagId == dto.Value).FirstOrDefault();
    var medicine = _unitOfWork.Medicines.FindById(dto.MedicineId);
    if (tag is null)
    {
      tag = _mapper.Map<Tag>(dto);
      //tag.Medicines = medicines!.ToList();
      _unitOfWork.Tags.Add(tag);
    } else
    {
      //tag.Medicines!.Concat(medicines!).Distinct(;
    }
    _unitOfWork.Save();
  }
}
