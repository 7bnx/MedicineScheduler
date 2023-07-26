using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.WPFUI.Commands;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MedicineScheduler.WPFUI.ViewModel;

public class PrescriptionsListViewModel : ViewModelBase
{
  public ICommand AddPrescriptionCommand { get; }
  public ICommand RemovePrescriptionCommand { get; }
  public ICommand EditPrescriptionCommand { get; }

  public PrescriptionDTO SelectedPrescription
  {
    get => _selectedPrescription;
    set
    {
      if (value == _selectedPrescription) return;
      _selectedPrescription = value;
      _prescriptionStore.CurrentSelected = value;
      OnPropertyChanged();
    }
  }

  public ReadOnlyObservableCollection<PrescriptionDTO> Prescriptions
  {
    get => _prescriptions ??= _prescriptionStore.Prescriptions;
    set
    {
      if (_prescriptions == value) return;
      _prescriptions = value;
      OnPropertyChanged();
    }
  }
  private ReadOnlyObservableCollection<PrescriptionDTO> _prescriptions = null!;
  private PrescriptionDTO _selectedPrescription = null!;
  private readonly PrescriptionStore _prescriptionStore;
  private readonly ILogger<PrescriptionsListViewModel> _logger;

  public PrescriptionsListViewModel
  (
    INavigationService addNavigationService,
    INavigationService editNavigationService,
    PrescriptionStore prescriptionStore,
    ILogger<PrescriptionsListViewModel> logger
  )
  {
    _prescriptionStore = prescriptionStore;
    _logger = logger;
    _logger.LogTrace($"{nameof(PrescriptionsListViewModel)} created");
    AddPrescriptionCommand = new NavigateCommand(addNavigationService);
    RemovePrescriptionCommand = new RelayCommand(_ => RemoveHandler());
    EditPrescriptionCommand = new NavigateCommand(editNavigationService);
    _prescriptionStore.OnPrescriptionsChanged += PrescriptionStore_OnPrescriptionsChanged;
  }

  private void PrescriptionStore_OnPrescriptionsChanged()
    => Prescriptions = _prescriptionStore.Prescriptions;

  public override void Dispose()
  {
    _prescriptionStore.OnPrescriptionsChanged -= PrescriptionStore_OnPrescriptionsChanged;
    _logger.LogTrace($"{nameof(PrescriptionsListViewModel)} disposed");
    base.Dispose();
  }

  private void RemoveHandler()
  {
    if (SelectedPrescription is null) return;
    var result = _prescriptionStore.Remove(SelectedPrescription);
    if (!result.IsOk)
      MessageBox.Show(result.Message);
  }
}