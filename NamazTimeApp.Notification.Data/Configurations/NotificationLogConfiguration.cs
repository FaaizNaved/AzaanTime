using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Notification;

namespace NamazTimeApp.Notification.Data.Configurations;

public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
{
    public void Configure(EntityTypeBuilder<NotificationLog> builder)
    {
        builder.ToTable("NotificationLog", "NAMAZTIMEAPP_NOTIFICATION");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrayerDate).IsRequired();
        builder.Property(x => x.NotificationTitle).HasMaxLength(200).IsRequired();
        builder.Property(x => x.NotificationBody).HasColumnType("text");
        builder.Property(x => x.SentOn).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();
        builder.Property(x => x.FailureReason).HasColumnType("text");
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasOne(x => x.DeviceRegistration)
            .WithMany()
            .HasForeignKey(x => x.DeviceRegistrationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PrayerType)
            .WithMany()
            .HasForeignKey(x => x.PrayerTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
