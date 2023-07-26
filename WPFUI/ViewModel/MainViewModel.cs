using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;

namespace MedicineScheduler.WPFUI.ViewModel;
public class MainViewModel : ViewModelBase
{
  public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
  public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

  private readonly NavigationStore _navigationStore;
  private readonly ModalNavigationStore _modalNavigationStore;

  public bool IsModalOpen => _modalNavigationStore.IsOpen;
  public MainViewModel
  (
    NavigationStore navigationStore,
    ModalNavigationStore modalNavigationStore,
    INavigationService initNavigationService
  )
  {
    _navigationStore = navigationStore;
    _modalNavigationStore = modalNavigationStore;
    _navigationStore.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    _modalNavigationStore.CurrentViewModelChanged += () => 
    { 
      OnPropertyChanged(nameof(CurrentModalViewModel)); 
      OnPropertyChanged(nameof(IsModalOpen)); 
    };
    initNavigationService.Navigate();
  }
}
