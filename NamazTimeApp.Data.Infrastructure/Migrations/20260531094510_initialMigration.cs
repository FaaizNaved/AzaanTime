using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamazTimeApp.Data.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NAMAZTIMEAPP_MASTER");

            migrationBuilder.EnsureSchema(
                name: "NAMAZTIMEAPP_TRANSACTION");

            migrationBuilder.EnsureSchema(
                name: "NAMAZTIMEAPP_NOTIFICATION");

            migrationBuilder.CreateTable(
                name: "AppSetting",
                schema: "NAMAZTIMEAPP_MASTER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SettingKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SettingValue = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "NAMAZTIMEAPP_MASTER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StateName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false),
                    TimeZone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrayerType",
                schema: "NAMAZTIMEAPP_MASTER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRegistration",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceUniqueId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Platform = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentFcmToken = table.Column<string>(type: "text", nullable: true),
                    AppVersion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NotificationEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LastSeenOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceRegistration_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "NAMAZTIMEAPP_MASTER",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrayerTime",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrayerDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Fajr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Sunrise = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Dhuhr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Asr = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Maghrib = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Isha = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrayerTime_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "NAMAZTIMEAPP_MASTER",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLog",
                schema: "NAMAZTIMEAPP_NOTIFICATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceRegistrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrayerTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrayerDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NotificationTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NotificationBody = table.Column<string>(type: "text", nullable: true),
                    SentOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FailureReason = table.Column<string>(type: "text", nullable: true),
                    CREATED_ID = table.Column<int>(type: "integer", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_ID = table.Column<int>(type: "integer", nullable: true),
                    UPDATED_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false),
                    DELETED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DELETED_BY = table.Column<int>(type: "integer", nullable: true),
                    RECORD_SOURCE_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationLog_DeviceRegistration_DeviceRegistrationId",
                        column: x => x.DeviceRegistrationId,
                        principalSchema: "NAMAZTIMEAPP_TRANSACTION",
                        principalTable: "DeviceRegistration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLog_PrayerType_PrayerTypeId",
                        column: x => x.PrayerTypeId,
                        principalSchema: "NAMAZTIMEAPP_MASTER",
                        principalTable: "PrayerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSetting_SettingKey",
                schema: "NAMAZTIMEAPP_MASTER",
                table: "AppSetting",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRegistration_DeviceUniqueId",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                table: "DeviceRegistration",
                column: "DeviceUniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRegistration_LocationId",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                table: "DeviceRegistration",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Code",
                schema: "NAMAZTIMEAPP_MASTER",
                table: "Location",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_DeviceRegistrationId",
                schema: "NAMAZTIMEAPP_NOTIFICATION",
                table: "NotificationLog",
                column: "DeviceRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_PrayerTypeId",
                schema: "NAMAZTIMEAPP_NOTIFICATION",
                table: "NotificationLog",
                column: "PrayerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrayerTime_LocationId_PrayerDate",
                schema: "NAMAZTIMEAPP_TRANSACTION",
                table: "PrayerTime",
                columns: new[] { "LocationId", "PrayerDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrayerType_Code",
                schema: "NAMAZTIMEAPP_MASTER",
                table: "PrayerType",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSetting",
                schema: "NAMAZTIMEAPP_MASTER");

            migrationBuilder.DropTable(
                name: "NotificationLog",
                schema: "NAMAZTIMEAPP_NOTIFICATION");

            migrationBuilder.DropTable(
                name: "PrayerTime",
                schema: "NAMAZTIMEAPP_TRANSACTION");

            migrationBuilder.DropTable(
                name: "DeviceRegistration",
                schema: "NAMAZTIMEAPP_TRANSACTION");

            migrationBuilder.DropTable(
                name: "PrayerType",
                schema: "NAMAZTIMEAPP_MASTER");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "NAMAZTIMEAPP_MASTER");
        }
    }
}
