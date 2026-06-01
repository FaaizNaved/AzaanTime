using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Master;

namespace NamazTimeApp.Master.Data.Configurations;

public class PrayerTypeConfiguration : IEntityTypeConfiguration<PrayerType>
{
    public void Configure(EntityTypeBuilder<PrayerType> builder)
    {
        builder.ToTable("PrayerType", "NAMAZTIMEAPP_MASTER");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.DisplayOrder).IsRequired();
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
