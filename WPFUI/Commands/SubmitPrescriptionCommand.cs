using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;
using System.Windows;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Commands;

internal abstract record SubmitPrescriptionCommand
(
  PrescriptionBaseViewModel PrescriptionBaseViewModel,
  INavigationService NavigationService,
  PrescriptionStore PrescriptionStore
) : CommandBase
{
  protected abstract OperationResult<PrescriptionDTO> ConcreteCommand(IPrescriptionDTO prescription);
  public override void Execute(object? parameter)
  {
    PrescriptionBaseViewModel.Validate();
    if (PrescriptionBaseViewModel.HasErrors) return;
    var operationResult = ConcreteCommand(PrescriptionBaseViewModel);
    if(!operationResult.IsOk)
    {
      MessageBox.Show(operationResult.Message);
    }
    NavigationService.Navigate();
  }
}
