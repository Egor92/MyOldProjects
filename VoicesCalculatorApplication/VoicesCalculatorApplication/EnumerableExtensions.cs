using System;
using System.Collections.Generic;
using System.Linq;

namespace VoicesCalculatorApplication
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> GetEqualItems<T>(this IEnumerable<T> source, IComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (comparer == null)
                comparer = Comparer<T>.Default;

            var equalItems = new List<T>();
            var list = source as IList<T> ?? new List<T>(source);
            
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i+1; j < list.Count; j++)
                {
                    var leftItem = list[i];
                    var rightItem = list[j];
                    if (comparer.Compare(leftItem, rightItem) == 0)
                    {
                        if (!equalItems.Contains(leftItem, comparer))
                            equalItems.Add(leftItem);
                        i++;
                    }
                }
            }
            return equalItems;
        }

        public static bool Contains<T>(this IEnumerable<T> source, T item, IComparer<T> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (comparer == null)
                comparer = Comparer<T>.Default;

            return source.Any(x => comparer.Compare(x, item) == 0);
        }
    }
}
