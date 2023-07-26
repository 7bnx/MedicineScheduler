using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using Microsoft.Extensions.Logging;

namespace MedicineScheduler.WPFUI.ViewModel;
public class HomeViewModel : ViewModelBase
{
  private readonly ActivePatientLocationStore _activePatientLocationStore;
  private readonly ILogger<HomeViewModel> _logger;

  public HomeViewModel
  (
    INavigationService navigationService,
    ActivePatientLocationStore activePatientLocationStore,
    ILogger<HomeViewModel> logger
  )
  {
    _activePatientLocationStore = activePatientLocationStore;
    _logger = logger;
    _logger.LogTrace($"{nameof(HomeViewModel)} created");
    if (_activePatientLocationStore.CurrentActive is null)
      navigationService.Navigate();
  }

  public override void Dispose()
  {
    _logger.LogTrace($"{nameof(HomeViewModel)} disposed");
    base.Dispose();
  }
}
