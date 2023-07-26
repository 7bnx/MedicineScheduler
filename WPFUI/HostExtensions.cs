using MedicineScheduler.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MedicineScheduler.WPFUI;

public static class HostExtensions
{
  public static async Task SetupDatabase(this IHost host)
  {
    await host.Services.GetRequiredService<EFContext>().Database.MigrateAsync();
  }

  public static async Task SetupAsync(this IHost host)
  {
    await host.SetupDatabase();
    host.Services.GetRequiredService<MainWindow>().Show();
  }

}

