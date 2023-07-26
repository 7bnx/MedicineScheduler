using MedicineScheduler.WPFUI.Services;
using System;

namespace MedicineScheduler.WPFUI.Commands;

internal record NavigateCommand
(
  INavigationService NavigationService, 
  Func<object?, bool>? CanExecuteCommand = null
) : CommandBase
{
  public override void Execute(object? parameter)
    => NavigationService.Navigate();
  public override bool CanExecute(object? parameter)
    => CanExecuteCommand == null || CanExecuteCommand(parameter);
}
