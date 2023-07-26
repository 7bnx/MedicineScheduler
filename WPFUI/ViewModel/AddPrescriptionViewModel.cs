using System.Windows;
using System.Windows.Input;
using MedicineScheduler.WPFUI.Commands;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.ServiceLayer.ValidationRules;

namespace MedicineScheduler.WPFUI.ViewModel;

public class AddPrescriptionViewModel : PrescriptionBaseViewModel
{
  public override string Title => "Add Prescription";
  public override ICommand SubmitPrescriptionCommand { get; init; } = null!;
  public override ICommand CancelCommand { get; init; } = null!;
  public AddPrescriptionViewModel
  (
    INavigationService closeNavigationService,
    PrescriptionDTOValidator validator,
    PrescriptionStore prescriptionStore,
    ActivePatientLocationStore activePatientLocationStore,
    MedicineStore medicineStore
  ) : base(validator, medicineStore)
  {
    if (activePatientLocationStore?.CurrentActive is null)
    {
      MessageBox.Show("Patient is not set");
      closeNavigationService.Navigate();
      return;
    }
    SubmitPrescriptionCommand = new AddPrescriptionCommand(this, closeNavigationService, prescriptionStore);
    CancelCommand = new NavigateCommand(closeNavigationService);
    PatientId = activePatientLocationStore.CurrentActive.PatientId;
  }

  public override void Dispose()
  {
    base.Dispose();
  }
}
