using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamazTimeApp.Data.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TblModifiedDeviceRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrayerPreferencesJson",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                table: "DeviceRegistration",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrayerPreferencesJson",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                table: "DeviceRegistration");
        }
    }
}
