using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedicineScheduler.DataAccessLayer;


public class ContextFactory// : IDesignTimeDbContextFactory<EFContext>
{
  //public EFContext CreateDbContext(string[] args)
  //{
  //  var optionsBuilder = new DbContextOptionsBuilder<EFContext>();

  //  // получаем конфигурацию из файла appsettings.json
  //  ConfigurationBuilder builder = new ConfigurationBuilder();
  //  builder.SetBasePath(Directory.GetCurrentDirectory());
  //  builder.AddJsonFile("appsettings.json");
  //  IConfigurationRoot config = builder.Build();

  //  // получаем строку подключения из файла appsettings.json
  //  string connectionString = config.GetConnectionString("DefaultConnection");
  //  optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
  //  return new EFContext(optionsBuilder.Options);
  //}
}
