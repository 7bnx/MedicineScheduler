using MedicineScheduler.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicineScheduler.DataAccessLayer.EntityConfiguration;
public class OrderRemainsConfiguration : IEntityTypeConfiguration<OrderRemains>
{
  public void Configure(EntityTypeBuilder<OrderRemains> builder)
  {
    builder
      .HasOne(or => or.Order)
      .WithOne(o => o.Remains)
      .HasForeignKey<OrderRemains>(t => t.OrderId);

    builder
      .HasKey(o => o.OrderId);
  }
}
