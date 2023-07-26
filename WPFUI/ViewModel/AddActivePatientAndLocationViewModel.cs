using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.ServiceLayer.ValidationRules;
using MedicineScheduler.WPFUI.Commands;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace MedicineScheduler.WPFUI.ViewModel;

public class AddActivePatientAndLocationViewModel
  : ViewModelBaseWithValidation<IActivePatientLocationDTO>, IActivePatientLocationDTO
{
  public int PatientId { get; set; }
  public int LocationId { get; set; }
  public ICommand AddPatientAndLocationCommand { get; set; } = null!;
  public ICommand CancelCommand { get; set; } = null!;
  private readonly INavigationService _closeNavigationService;
  private readonly ActivePatientLocationStore _activePatientLocationStore = null!;
  private string _patientName = null!, _locationName = null!;
  public IEnumerable<PatientDTO> Patients { get; set; } = null!;
  public IEnumerable<LocationDTO> Locations { get; set; } = null!;
  public string PatientName
  {
    get => _patientName!;
    set
    {
      _patientName = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public string LocationName
  {
    get => _locationName!;
    set
    {
      _locationName = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public AddActivePatientAndLocationViewModel
  (
    INavigationService closeNavigationService,
    ActivePatientLocationStore activePatientLocationStore,
    ActivePatientLocationValidator validator
  ) : base(validator)
  {
    _closeNavigationService = closeNavigationService;
    _activePatientLocationStore = activePatientLocationStore;

    AddPatientAndLocationCommand = new AddPatientLocationCommand(this, _activePatientLocationStore, _closeNavigationService);
    CancelCommand = new NavigateCommand(closeNavigationService, _ => _activePatientLocationStore.CurrentActive is not null);

    if (_activePatientLocationStore.CurrentActive is null) return;

    Patients = activePatientLocationStore.Patients;
    Locations = activePatientLocationStore.Locations;
    PatientName = _activePatientLocationStore.CurrentActive.PatientName;
    LocationName = _activePatientLocationStore.CurrentActive.LocationName;
  }

  public override void Dispose()
  {
    base.Dispose();
  }
}
