using System;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Stores;

public class ModalNavigationStore
{
  private ViewModelBase _currentViewModel = null!;
  public ViewModelBase CurrentViewModel
  {
    get => _currentViewModel;
    set
    {
      if (!_isClosed)
      {
        _currentViewModel?.Dispose();
        _currentViewModel = value;
        OnCurrentViewModelChanged();
      }else
        _isClosed = false;
    }
  }
  private bool _isClosed;
  public bool IsOpen => CurrentViewModel != null;

  public event Action? CurrentViewModelChanged;

  public void Close()
  {
    if(_currentViewModel is null) _isClosed = true;
    else CurrentViewModel = null!;
  }

  private void OnCurrentViewModelChanged()
  {
    CurrentViewModelChanged?.Invoke();
  }
}
