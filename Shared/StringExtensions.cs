using System;
using System.Globalization;

[assembly: CLSCompliant(false)]
namespace SharedHelpers
{
    public static class StringExtensions
    {
        public static int ToInt(this string input)
        {
            return int.Parse(input, CultureInfo.InvariantCulture);
        }
    }
}
