using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamazTimeApp.Core.Infrastructure.Helpers
{
    public static class MappingExtensions
    {
        public static int ToIntOrDefault(this string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }

        public static bool ToBoolActive(this string value)
        {
            return string.Equals(value, "Active", StringComparison.OrdinalIgnoreCase);
        }
    }
}
