using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicineScheduler.WPFUI.Stores;

public class MedicineStore
{
  public event Action? OnMedicinesChanged;
  public event Action<MedicineDTO>? OnMedicinesAdded;

  private readonly IMedicineService _medicineService;

  public List<MedicineDTO> Medicines
  {
    get => _medicines ??= _medicineService.Get().Value!.ToList();
    private set
    {
      if (_medicines != value)
      {
        _medicines = value;
        OnMedicinesChanged?.Invoke();
      }
    }
  }
  private List<MedicineDTO> _medicines = null!;

  public MedicineStore(IMedicineService medicineService)
  {
    _medicineService = medicineService;
  }

  public OperationResult<MedicineDTO> Add(string title)
    => Add(new MedicineDTO() { Title = title });

  public OperationResult<MedicineDTO> Add(MedicineDTO dto)
  {
    var newDto = dto with
    {
      MedicineId = Medicines.FirstOrDefault(m => m.Title == dto.Title)?.MedicineId ?? 0
    };
    if (newDto.MedicineId != 0)
      return new(newDto);
    var result = _medicineService.Add(newDto);
    if (!result.IsOk || result.Value is null)
      return result;
    Medicines.Add(result.Value);
    OnMedicinesAdded?.Invoke(result.Value);
    return result;
  }
}
