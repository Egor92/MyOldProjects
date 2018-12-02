using System;
using System.Linq;

namespace VoicesCalculatorApplication
{
    public static class StringExtensions
    {
        public static string[] SplitIgnoringEmptyChars(this string source, params char[] separators)
        {
            return source.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}
