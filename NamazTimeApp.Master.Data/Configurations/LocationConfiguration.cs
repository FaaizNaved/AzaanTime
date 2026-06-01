using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Master;

namespace NamazTimeApp.Master.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location", "NAMAZTIMEAPP_MASTER");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.StateName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CountryName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Latitude).HasPrecision(10, 7);
        builder.Property(x => x.Longitude).HasPrecision(10, 7);
        builder.Property(x => x.TimeZone).HasMaxLength(100).IsRequired();
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
