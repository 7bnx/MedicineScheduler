using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace MedicineScheduler.WPFUI;

public partial class App : Application
{
  private readonly IHost _host;
  private readonly ILogger _log;
  public App()
  {
    _host = new CompositionRoot().Host;
    _log = _host.Services.GetRequiredService<ILogger<App>>();
  }

  protected async override void OnStartup(StartupEventArgs e)
  {
    _log.LogInformation("Startup");
    await _host.SetupAsync();
    await _host.StartAsync();
    base.OnStartup(e);
  }
  protected override async void OnExit(ExitEventArgs e)
  {
    await _host.StopAsync();
    base.OnExit(e);
    _log.LogInformation("Exit");
  }
}