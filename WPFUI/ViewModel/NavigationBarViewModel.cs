using System.Windows.Input;
using MedicineScheduler.WPFUI.Commands;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;

namespace MedicineScheduler.WPFUI.ViewModel;

public class NavigationBarViewModel : ViewModelBase
{
  public ICommand NavigateHomeCommand { get; }
  public ICommand NavigateStorageCommand { get; }
  public ICommand NavigatePrescriptionCommand { get; }
  public ICommand NavigateAddPatientLocationCommand { get; }

  public string PatientName
    => string.Join(", ", 
      _activePatientLocationStore?.CurrentActive?.PatientName!, _activePatientLocationStore?.CurrentActive?.LocationName!);
  private readonly ActivePatientLocationStore _activePatientLocationStore;

  public NavigationBarViewModel(
    ActivePatientLocationStore activePatientLocationStore,
    INavigationService homeNavigationService,
    INavigationService storageNavigationService,
    INavigationService prescriptionNavigationService,
    INavigationService addPatientLocationNavigationService)
  {
    NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    NavigateStorageCommand = new NavigateCommand(storageNavigationService);
    NavigatePrescriptionCommand = new NavigateCommand(prescriptionNavigationService);
    NavigateAddPatientLocationCommand = new NavigateCommand(addPatientLocationNavigationService);
    _activePatientLocationStore = activePatientLocationStore;
    _activePatientLocationStore.OnCurrentActiveChanged += ActivePatientLocationStore_CurrentActiveChanged;
  }

  private void ActivePatientLocationStore_CurrentActiveChanged()
  {
    OnPropertyChanged(nameof(PatientName));
  }

  public override void Dispose()
  {
    _activePatientLocationStore.OnCurrentActiveChanged -= ActivePatientLocationStore_CurrentActiveChanged;
    base.Dispose();
  }
}
