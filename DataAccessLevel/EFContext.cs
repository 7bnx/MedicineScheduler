using MedicineScheduler.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MedicineScheduler.DataAccessLayer;

public class EFContext : DbContext
{
  public DbSet<Medicine> Medicines { get; set; }
  public DbSet<Location> Locations { get; set; }
  public DbSet<ActivePatientLocation> ActivePatientLocations { get; set; }
  public DbSet<Patient> Patients { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderRemains> OrdersRemains { get; set; }
  public DbSet<Prescription> Prescriptions { get; set; }
  public DbSet<DosageForm> DosageForms { get; set; }
  public DbSet<Tag> Tags { get; set; }

  public EFContext(DbContextOptions<EFContext> options) : base(options)
  {

  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      foreach (var entityProperty in entityType.GetProperties())
      {
        if (entityProperty.ClrType == typeof(DateTime))
          entityProperty.SetColumnType("date");
        if (entityProperty.ClrType == typeof(decimal))
        {
          entityProperty.SetPrecision(9);
          entityProperty.SetScale(2);
          if (Database.IsSqlite())
            entityProperty.SetProviderClrType(typeof(double));
        }
        if (entityProperty.ClrType.IsEnum)
          entityProperty.SetProviderClrType(typeof(string));
      }
    }

    base.OnModelCreating(modelBuilder);
  }
}
