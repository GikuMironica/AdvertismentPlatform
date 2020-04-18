using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsAny(this string target, IEnumerable<string> values)
        {
            return values.Contains(target, StringComparer.OrdinalIgnoreCase);
        }
        public static bool EqualsAny(this string target, params string[] values)
        {
            return target.EqualsAny((IEnumerable<string>)values);
        }

    }
}
