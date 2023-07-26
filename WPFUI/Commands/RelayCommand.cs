using System;
using MedicineScheduler.WPFUI.Commands;

namespace MedicineScheduler.WPFUI;
internal record RelayCommand
(
  Action<object?> ExecutionCommand,
  Func<object?, bool>? CanExecuteCommand = null
) : CommandBase
{

  public override bool CanExecute(object? parameter)
    => CanExecuteCommand == null || CanExecuteCommand(parameter);

  public override void Execute(object? parameter)
    => ExecutionCommand(parameter);
}
