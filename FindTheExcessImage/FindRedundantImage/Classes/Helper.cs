using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace FindTheExcessImage
{
	static class Helper
	{
		public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> enumerable, int count)
		{
			var rnd = new Random();
			var list = enumerable.ToList();
			var result = new List<T>();
			for (int i = 0; i < count; i++)
			{
				int number = rnd.Next(list.Count);
				result.Add(list[number]);
				list.RemoveAt(number);
			}
			return result;
		}

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            var rnd = new Random();
            int number = rnd.Next(enumerable.Count());
            return enumerable.ElementAt(number);
        }

		public static IEnumerable<T> Mix<T>(this IEnumerable<T> originalArray)
		{
		    if (originalArray == null) yield break;

		    var rnd = new Random();
		    var randDictionary = new Dictionary<int, int>();
		    for (int I = 0; I < originalArray.Count(); I++)
		    {
		        randDictionary.Add(I, rnd.Next(32767));
		    }
		    KeyValuePair<int, int>[] randArray = randDictionary.OrderBy(x => x.Value).ToArray();
		    for (int I = 0; I < originalArray.Count(); I++)
		    {
		        yield return originalArray.ElementAt(randArray[I].Key);
		    }
		}

        public static IEnumerable<T> Mix<T>(this IEnumerable<T> originalArray, int from, int to)
        {
            if (originalArray == null)
                throw new ArgumentNullException("originalArray");
            if (from < 0)
                throw new ArgumentOutOfRangeException("from", "Argument 'from' mustn't be negative");
            if (to > originalArray.Count() - 1)
                throw new ArgumentOutOfRangeException("to", "Argument 'to' must be lower or equals than count of originalArray");
            int mixingElementsCount = to - from + 1;
            if (mixingElementsCount <= 0)
                throw new ArgumentException("Argument 'from' must be lower or equals than argument 'to'");

            var mixingSubArray = new T[mixingElementsCount];
            for (int i = 0; i < mixingElementsCount; i++)
                mixingSubArray[i] = originalArray.ElementAt(from + i);
            var mixedSubArray = mixingSubArray.Mix().ToArray();
            for (int i = 0; i < originalArray.Count(); i++)
            {
                if (from <= i && i <= to)
                    yield return mixedSubArray.ElementAt(i - from);
                else
                    yield return originalArray.ElementAt(i);
            }
        }
	}
}
