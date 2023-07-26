using MedicineScheduler.DataAccessLayer;
using MedicineScheduler.ServiceLayer.Services;
using MedicineScheduler.ServiceLayer.ValidationRules;
using MedicineScheduler.WPFUI.Services;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.WPFUI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace MedicineScheduler.WPFUI;

public class CompositionRoot
{
  public IHost Host { get; init; }
  public CompositionRoot()
  {
    Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true)
        .AddJsonFile($"appsettings.Development.json", true)
        .Build();

      services
      .AddSingleton<IConfiguration>(_ => config)
      .AddSingleton<NavigationStore>()
      .AddSingleton<ModalNavigationStore>()
      .AddSingleton<MainViewModel>(s => new
      (
        s.GetRequiredService<NavigationStore>(), 
        s.GetRequiredService<ModalNavigationStore>(), 
        CreateHomeNavigationService(s))
      )
      .AddSingleton<CloseModalNavigationService>()
      .AddSingleton<IPrescriptionService, PrescriptionService>()
      .AddSingleton<IActivePatientLocationService, ActivePatientLocationService>()
      .AddSingleton<ActivePatientLocationStore>()
      .AddSingleton<PrescriptionStore>()
      .AddSingleton<IMedicineService, MedicineService>()
      .AddSingleton<MedicineStore>()
      .AddSingleton<DosageFormStore>()
      .AddSingleton<IDosageFormService, DosageFormService>()
      .AddSingleton<ActivePatientLocationValidator>()
      .AddSingleton<PrescriptionDTOValidator>()
      .AddDbContext<EFContext>((s, options) =>
      {
        options.UseSqlite(s.GetRequiredService<IConfiguration>().GetConnectionString("SqLiteConnectionString"));
      })
      .AddScoped<IUnitOfWork, UnitOfWork>()
      .AddTransient<HomeViewModel>(s => new HomeViewModel
      (
        CreateAddPatientLocationNavigationService(s),
        s.GetRequiredService<ActivePatientLocationStore>(),
        s.GetRequiredService<ILogger<HomeViewModel>>()
      ))
      .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
      .AddTransient<PrescriptionsListViewModel>(s => new PrescriptionsListViewModel
      (
        CreateAddPrescriptionNavigationService(s),
        CreateEditPrescriptionNavigationService(s),
        s.GetRequiredService<PrescriptionStore>(),
        s.GetRequiredService<ILogger<PrescriptionsListViewModel>>()
       ))
      .AddTransient<AddPrescriptionViewModel>(s => new AddPrescriptionViewModel
      (
        s.GetRequiredService<CloseModalNavigationService>(),
        s.GetRequiredService<PrescriptionDTOValidator>(),
        s.GetRequiredService<PrescriptionStore>(),
        s.GetRequiredService<ActivePatientLocationStore>(),
        s.GetRequiredService<MedicineStore>()
      ))
      .AddTransient<EditPrescriptionViewModel>(s => new EditPrescriptionViewModel
      (
        s.GetRequiredService<CloseModalNavigationService>(),
        s.GetRequiredService<PrescriptionDTOValidator>(),
        s.GetRequiredService<PrescriptionStore>(),
        s.GetRequiredService<ActivePatientLocationStore>(),
        s.GetRequiredService<MedicineStore>()
      ))
      .AddTransient<AddActivePatientAndLocationViewModel>(s => new AddActivePatientAndLocationViewModel
      (
        s.GetRequiredService<CloseModalNavigationService>(),
        s.GetRequiredService<ActivePatientLocationStore>(),
        s.GetRequiredService<ActivePatientLocationValidator>())
      )
      .AddTransient<StorageModelView>()
      .AddSingleton<NavigationBarViewModel>(CreateNavigationBarViewModel)
      .AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() })
      .AddLogging(c => c.AddNLog(new NLogLoggingConfiguration(config.GetSection("NLog"))));

    }).Build();
  }

  private static NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
  {
    return new NavigationBarViewModel(
      serviceProvider.GetRequiredService<ActivePatientLocationStore>(),
      CreateHomeNavigationService(serviceProvider),
      CreateStorageNavigationService(serviceProvider),
      CreatePrescriptionNavigationService(serviceProvider),
      CreateAddPatientLocationNavigationService(serviceProvider));
  }

  private static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
  {
    return new LayoutNavigationService<HomeViewModel>
    (
      serviceProvider.GetRequiredService<NavigationStore>(),
      () => serviceProvider.GetRequiredService<HomeViewModel>(),
      () => CreateNavigationBarViewModel(serviceProvider)
    );
  }

  private static INavigationService CreatePrescriptionNavigationService(IServiceProvider serviceProvider)
  {
    return new LayoutNavigationService<PrescriptionsListViewModel>
    (
      serviceProvider.GetRequiredService<NavigationStore>(),
      () => serviceProvider.GetRequiredService<PrescriptionsListViewModel>(),
      () => CreateNavigationBarViewModel(serviceProvider)
    );
  }

  private static INavigationService CreateAddPrescriptionNavigationService(IServiceProvider serviceProvider)
  {
    return new ModalNavigationService<AddPrescriptionViewModel>
    (
      serviceProvider.GetRequiredService<ModalNavigationStore>(),
      () => serviceProvider.GetRequiredService<AddPrescriptionViewModel>()
    );
  }

  private static INavigationService CreateEditPrescriptionNavigationService(IServiceProvider serviceProvider)
  {
    return new ModalNavigationService<EditPrescriptionViewModel>
    (
      serviceProvider.GetRequiredService<ModalNavigationStore>(),
      () => serviceProvider.GetRequiredService<EditPrescriptionViewModel>()
    );
  }

  private static INavigationService CreateStorageNavigationService(IServiceProvider serviceProvider)
  {
    return new LayoutNavigationService<StorageModelView>
    (
      serviceProvider.GetRequiredService<NavigationStore>(),
      () => serviceProvider.GetRequiredService<StorageModelView>(),
      () => CreateNavigationBarViewModel(serviceProvider)
    );
  }

  private static INavigationService CreateAddPatientLocationNavigationService(IServiceProvider serviceProvider)
  {
    return new ModalNavigationService<AddActivePatientAndLocationViewModel>
    (
      serviceProvider.GetRequiredService<ModalNavigationStore>(),
      () => serviceProvider.GetRequiredService<AddActivePatientAndLocationViewModel>()
    );
  }
}