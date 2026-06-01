using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Transaction;

namespace NamazTimeApp.Transaction.Data.Configurations;

public class PrayerTimeConfiguration : IEntityTypeConfiguration<PrayerTime>
{
    public void Configure(EntityTypeBuilder<PrayerTime> builder)
    {
        builder.ToTable("PrayerTime", "NAMAZTIMEAPP_TRANSACTION");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrayerDate).IsRequired();
        builder.Property(x => x.Fajr).IsRequired();
        builder.Property(x => x.Sunrise).IsRequired();
        builder.Property(x => x.Dhuhr).IsRequired();
        builder.Property(x => x.Asr).IsRequired();
        builder.Property(x => x.Maghrib).IsRequired();
        builder.Property(x => x.Isha).IsRequired();
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.LocationId, x.PrayerDate }).IsUnique();
    }
}
