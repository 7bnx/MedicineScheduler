using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using MedicineScheduler.ServiceLayer.Services;

namespace MedicineScheduler.WPFUI.Stores;

public class ActivePatientLocationStore
{
  public event Action? OnCurrentActiveChanged;
  public event Action? OnPatientsChanged;
  public event Action? OnLocationsChanged;

  private readonly IActivePatientLocationService _activePatientLocationService;
  private readonly IPatientService _patientService;
  private readonly ILocationService _locationService;
  private ActivePatientLocationDTO _currentActive = null!;
  private List<PatientDTO> _patients = null!;
  private List<LocationDTO> _locations = null!;

  public ActivePatientLocationStore
  (
    IActivePatientLocationService activePatientLocationService,
    IPatientService patientService,
    ILocationService locationService
  )
  {
    _activePatientLocationService = activePatientLocationService;
    _patientService = patientService;
    _locationService = locationService;
  }

  public OperationResult<ActivePatientLocationDTO> Set(ActivePatientLocationDTO newActivePatientAndLocation)
  {
    if (newActivePatientAndLocation != CurrentActive)
    {
      var operationResult = _activePatientLocationService.Set(newActivePatientAndLocation);
      if (operationResult.IsOk && operationResult.Value is not null)
      {
        CurrentActive = operationResult.Value;
        if (!Patients.Any(p => p.PatientName == CurrentActive.PatientName))
          _patients.Add(new() { PatientId = CurrentActive.PatientId, PatientName = CurrentActive.PatientName });
        if (!Locations.Any(l => l.LocationName == CurrentActive.LocationName))
          _locations.Add(new() { LocationId = CurrentActive.LocationId, LocationName = CurrentActive.LocationName });
      }
      return operationResult!;
    }
    return new(newActivePatientAndLocation);
  }

  public ActivePatientLocationDTO CurrentActive
  {
    get => _currentActive ??= _activePatientLocationService.Get().Value!;
    set
    {
      if (_currentActive == value) return;
      _currentActive = value;
      OnCurrentActiveChanged?.Invoke();
    }
  }

  public IReadOnlyList<PatientDTO> Patients
    => _patients ??= _patientService.Get().Value!;

  public IReadOnlyList<LocationDTO> Locations 
    => _locations ??= _locationService.Get().Value!;
}
