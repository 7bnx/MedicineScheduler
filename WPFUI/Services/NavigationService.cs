using System;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;

namespace MedicineScheduler.WPFUI.Services;

public class NavigationService<TViewModel> : INavigationService
  where TViewModel : ViewModelBase
{
  private readonly NavigationStore _navigationStore;
  private readonly Func<TViewModel> _createViewModel;
  public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
  {
    _navigationStore = navigationStore;
    _createViewModel = createViewModel;
  }
  public void Navigate()
    => _navigationStore.CurrentViewModel = _createViewModel();
}
