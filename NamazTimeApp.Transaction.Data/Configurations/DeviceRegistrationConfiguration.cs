using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Transaction;

namespace NamazTimeApp.Transaction.Data.Configurations;

public class DeviceRegistrationConfiguration : IEntityTypeConfiguration<DeviceRegistration>
{
    public void Configure(EntityTypeBuilder<DeviceRegistration> builder)
    {
        builder.ToTable("DeviceRegistration", "NAMAZTIMEAPP_TRANSACTION");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeviceUniqueId).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Platform).HasMaxLength(20).IsRequired();
        builder.Property(x => x.CurrentFcmToken).HasColumnType("text");
        builder.Property(x => x.AppVersion).HasMaxLength(20).IsRequired();
        builder.Property(x => x.NotificationEnabled).IsRequired();
        builder.Property(x => x.LastSeenOn).IsRequired();
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.DeviceUniqueId).IsUnique();
    }
}
