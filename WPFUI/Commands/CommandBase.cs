using System;
using System.Windows.Input;

namespace MedicineScheduler.WPFUI.Commands;

internal abstract record CommandBase : ICommand
{
  public event EventHandler? CanExecuteChanged;
  private bool _canExecute;
  public virtual bool CanExecute(object? parameter) => true;

  public abstract void Execute(object? parameter);

  protected void OnCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, new EventArgs());
}
