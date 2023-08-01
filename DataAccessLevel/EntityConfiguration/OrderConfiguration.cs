using MedicineScheduler.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicineScheduler.DataAccessLayer.EntityConfiguration;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder
      .HasOne(o => o.Remains)
      .WithOne(o => o.Order)
      .HasForeignKey<Order>(t => t.OrderId);
    var tt = builder.Property(o => o.PriceTotal);

  }
}
