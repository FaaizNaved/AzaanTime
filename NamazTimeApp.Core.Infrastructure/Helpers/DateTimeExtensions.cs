using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NamazTimeApp.Core.Infrastructure.Helpers
{
    public static class DateTimeExtensions
    {
        private static readonly TimeZoneInfo IST = GetIstTimeZone();

        private static TimeZoneInfo GetIstTimeZone()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")
                : TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        }

        public static (DateTime FromUtc, DateTime ToUtc) ToUtcRange(
            this (DateTime From, DateTime To) range)
        {
            var fromIst = range.From.Date;
            var toIst = range.To.Date.AddDays(1);

            var fromUtc = new DateTimeOffset(fromIst, IST.GetUtcOffset(fromIst)).UtcDateTime;
            var toUtc = new DateTimeOffset(toIst, IST.GetUtcOffset(toIst)).UtcDateTime;

            return (fromUtc, toUtc);
        }

        public static DateTime ConvertUtcToIst(DateTime utcDate)
        {
            if (utcDate.Kind == DateTimeKind.Unspecified)
            {
                utcDate = DateTime.SpecifyKind(
                    utcDate,
                    DateTimeKind.Utc);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, IST);
        }

        public static DateTime? ConvertUtcToIst(DateTime? utcDate)
        {
            if (!utcDate.HasValue)
                return null;

            return ConvertUtcToIst(utcDate.Value);
        }

        public static bool TryParseIstDateToUtc(
            string value,
            out DateTime utcDate)
        {
            utcDate = default;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var success = DateTime.TryParseExact(
                value,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsedDate);

            if (!success)
                return false;

            var offset = IST.GetUtcOffset(parsedDate);

            utcDate = new DateTimeOffset(parsedDate, offset).UtcDateTime;

            return true;
        }
    }
}
