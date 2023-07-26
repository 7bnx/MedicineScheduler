using MedicineScheduler.Common;
using MedicineScheduler.Common.Enums;
using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicineScheduler.WPFUI.Stores;

public class DosageFormStore
{
  public event Action? OnDosageFormsChanged;
  public event Action<DosageFormDTO>? OnDosageFormAdded;
  private readonly IDosageFormService _dosageFormService;
  private List<DosageFormDTO> _dosageForms = null!;
  public List<DosageFormDTO> DosageForms
  {
    get => _dosageForms ??= _dosageFormService.Get()!.Value!.ToList();
    private set
    {
      if (_dosageForms != value)
      {
        _dosageForms = value;
        OnDosageFormsChanged?.Invoke();
      }
    }
  }

  public DosageFormStore(IDosageFormService dosageFormService)
  {
    _dosageFormService = dosageFormService;
  }

  public OperationResult<DosageFormDTO> Add(DosageType type, DosageUnit unit, double value)
    => Add(new() { Type = type, Unit = unit, Value = value });

  public OperationResult<DosageFormDTO> Add(DosageFormDTO dto)
  {
    var storedDosage = DosageForms.FirstOrDefault(d => dto.Type == d.Type && dto.Unit == d.Unit && dto.Value == d.Value);
    if (storedDosage is not null)
      return new(storedDosage with { });
    var result = _dosageFormService.Add(dto);
    if (result.IsOk && result.Value is not null)
      OnDosageFormAdded?.Invoke(result.Value);
    return result;
  }
}
