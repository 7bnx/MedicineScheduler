using MedicineScheduler.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicineScheduler.DataAccessLayer.EntityConfiguration;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
  public void Configure(EntityTypeBuilder<Tag> builder)
  {
    builder
      .Property(t => t.TagId).HasMaxLength(25);

    builder
      .HasKey(t => t.TagId);
  }
}
