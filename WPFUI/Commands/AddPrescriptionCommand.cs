using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.Common;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Commands;

internal record AddPrescriptionCommand : SubmitPrescriptionCommand
{
  public AddPrescriptionCommand
  (
    PrescriptionBaseViewModel PrescriptionBaseViewModel,
    INavigationService NavigationService,
    PrescriptionStore PrescriptionStore
  ) : base(PrescriptionBaseViewModel, NavigationService, PrescriptionStore) { }
  protected override OperationResult<PrescriptionDTO> ConcreteCommand(IPrescriptionDTO prescription)
     => PrescriptionStore.Add(prescription);
}
