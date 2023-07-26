using System.Windows.Input;
using MedicineScheduler.WPFUI.Commands;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.ServiceLayer.ValidationRules;

namespace MedicineScheduler.WPFUI.ViewModel;

public class EditPrescriptionViewModel : PrescriptionBaseViewModel
{
  public override string Title => "Edit Prescription";
  public override ICommand SubmitPrescriptionCommand { get; init; } = null!;
  public override ICommand CancelCommand { get; init; } = null!;
  public EditPrescriptionViewModel
  (
    INavigationService closeNavigationService,
    PrescriptionDTOValidator validator,
    PrescriptionStore prescriptionStore,
    ActivePatientLocationStore activePatientLocationStore,
    MedicineStore medicineStore
  ) : base(validator, medicineStore)
  {
    PatientId = activePatientLocationStore.CurrentActive.PatientId;
    SubmitPrescriptionCommand = new EditPrescriptionCommand(this, closeNavigationService, prescriptionStore);
    CancelCommand = new NavigateCommand(closeNavigationService);

    PrescriptionId = prescriptionStore.CurrentSelected.PrescriptionId;
    PatientId = prescriptionStore.CurrentSelected.PatientId;
    MedicineTitle = prescriptionStore.CurrentSelected.MedicineTitle;
    MedicineId = prescriptionStore.CurrentSelected.MedicineId;
    DosageFormType = prescriptionStore.CurrentSelected.DosageFormType;
    DosageFormId = prescriptionStore.CurrentSelected.DosageFormId;
    DosageFormValue = prescriptionStore.CurrentSelected.DosageFormValue;
    DosageFormUnit = prescriptionStore.CurrentSelected.DosageFormUnit;
    AmountMedication = prescriptionStore.CurrentSelected.AmountMedication;
    MedicationsPerDay = prescriptionStore.CurrentSelected.MedicationsPerDay;
    StartDate = prescriptionStore.CurrentSelected.StartDate;
    EndDate = prescriptionStore.CurrentSelected.EndDate;
    PrescriptionDate = prescriptionStore.CurrentSelected.PrescriptionDate;
    Note = prescriptionStore.CurrentSelected.Note;
  }

  public override void Dispose()
  {
    base.Dispose();
  }
}
