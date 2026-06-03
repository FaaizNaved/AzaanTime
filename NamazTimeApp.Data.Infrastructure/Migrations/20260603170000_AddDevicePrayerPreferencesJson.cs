using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamazTimeApp.Data.Infrastructure.Migrations;

public partial class AddDevicePrayerPreferencesJson : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "PrayerPreferencesJson",
            schema: "NAMAZTIMEAPP_TRANSACTION",
            table: "DeviceRegistration",
            type: "text",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PrayerPreferencesJson",
            schema: "NAMAZTIMEAPP_TRANSACTION",
            table: "DeviceRegistration");
    }
}
