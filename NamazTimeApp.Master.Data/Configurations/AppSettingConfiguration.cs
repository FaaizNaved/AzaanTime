using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NamazTimeApp.Master;

namespace NamazTimeApp.Master.Data.Configurations;

public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
{
    public void Configure(EntityTypeBuilder<AppSetting> builder)
    {
        builder.ToTable("AppSetting", "NAMAZTIMEAPP_MASTER");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SettingKey).HasMaxLength(100).IsRequired();
        builder.Property(x => x.SettingValue).HasColumnType("text").IsRequired();
        builder.Property(x => x.Description).HasColumnType("text");
        builder.Property(x => x.RECORD_SOURCE_NAME).HasMaxLength(100);

        builder.HasIndex(x => x.SettingKey).IsUnique();
    }
}
