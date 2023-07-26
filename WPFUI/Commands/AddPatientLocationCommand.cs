using MedicineScheduler.ServiceLayer.DTO;
using System.Linq;
using System.Windows;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Commands;

internal record AddPatientLocationCommand
(
  AddActivePatientAndLocationViewModel ViewModel,
  ActivePatientLocationStore ActivePatientLocationStore,
  INavigationService ClosingNavigationService
) : CommandBase
{
  public override void Execute(object? parameter)
  {
    ActivePatientLocationDTO tmpDto = new()
    {
      LocationName = ViewModel.LocationName,
      PatientName = ViewModel.PatientName,
      LocationId = ViewModel.Locations?
        .FirstOrDefault(l => l.LocationName == ViewModel.LocationName)?.LocationId ?? 0,
      PatientId = ViewModel.Patients?
        .FirstOrDefault(l => l.PatientName == ViewModel.PatientName)?.PatientId ?? 0
    };
    ViewModel.Validate();
    if (ViewModel.HasErrors) return;
    var operationResult = ActivePatientLocationStore.Set(tmpDto);
    if (!operationResult.IsOk)
    {
      MessageBox.Show($"{operationResult.Exception?.Message}");
    }

    ClosingNavigationService.Navigate();
  }
}
