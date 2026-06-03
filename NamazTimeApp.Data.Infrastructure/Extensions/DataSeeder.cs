using Microsoft.EntityFrameworkCore;
using NamazTimeApp.Core;
using NamazTimeApp.Master;

namespace NamazTimeApp.Data.Infrastructure.Extensions;

public static class DataSeeder
{
    public static void Seed(AppMigrationDbContext context)
    {
        SeedPrayerTypes(context);
    }

    private static void SeedPrayerTypes(AppMigrationDbContext context)
    {
        if (context.PrayerTypes.Any())
        {
            return;
        }

        var prayerTypes = new[]
        {
            CreatePrayerType("FAJR", "Fajr", 1),
            CreatePrayerType("SUNRISE", "Sunrise", 2),
            CreatePrayerType("DHUHR", "Dhuhr", 3),
            CreatePrayerType("ASR", "Asr", 4),
            CreatePrayerType("MAGHRIB", "Maghrib", 5),
            CreatePrayerType("ISHA", "Isha", 6)
        };

        context.PrayerTypes.AddRange(prayerTypes);
        context.SaveChanges();
    }

    private static PrayerType CreatePrayerType(string code, string name, int order) =>
        new()
        {
            Id = Guid.NewGuid(),
            Code = code,
            Name = name,
            DisplayOrder = order,
            CREATED_ID = Constants.DatastubConstants.SYSTEM_USER_ID,
            IS_ACTIVE = true,
            RECORD_SOURCE_NAME = Constants.Common.RECORD_SOURCE
        };
}
