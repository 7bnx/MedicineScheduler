using System;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Stores;

public class NavigationStore
{
  private ViewModelBase _currentViewModel = null!;
  public ViewModelBase CurrentViewModel
  {
    get => _currentViewModel;
    set
    {
      _currentViewModel?.Dispose();
      _currentViewModel = value;
      OnCurrentViewModelChanged();
    }
  }

  public event Action? CurrentViewModelChanged;

  private void OnCurrentViewModelChanged()
  {
    CurrentViewModelChanged?.Invoke();
  }
}
