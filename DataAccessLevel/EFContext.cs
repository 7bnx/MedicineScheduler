using MedicineScheduler.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

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
    modelBuilder.Entity<Tag>()
      .Property(t => t.TagId).HasMaxLength(25);
    modelBuilder.Entity<Tag>()
      .HasKey(t => t.TagId);
    modelBuilder.Entity<OrderRemains>()
      .HasOne(or => or.Order)
      .WithOne(o => o.Remains)
      .HasForeignKey<OrderRemains>(t => t.OrderId);
    modelBuilder.Entity<Order>()
      .HasOne(o => o.Remains)
      .WithOne(o => o.Order)
      .HasForeignKey<Order>(t => t.OrderId);
    modelBuilder.Entity<OrderRemains>()
      .HasKey(o => o.OrderId);
    base.OnModelCreating(modelBuilder);
  }
}
