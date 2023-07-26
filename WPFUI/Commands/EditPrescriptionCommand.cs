using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Commands;

internal record EditPrescriptionCommand : SubmitPrescriptionCommand
{
  public EditPrescriptionCommand
  (
    PrescriptionBaseViewModel PrescriptionBaseViewModel,
    INavigationService NavigationService,
    PrescriptionStore PrescriptionStore
  ) : base(PrescriptionBaseViewModel, NavigationService, PrescriptionStore) { }
  protected override OperationResult<PrescriptionDTO> ConcreteCommand(IPrescriptionDTO prescription)
     => PrescriptionStore.Update(prescription);
}
