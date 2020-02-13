using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(Enum), typeof(int))]
	public class EnumToIntConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToInt32((Enum)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return targetType
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Single(x => System.Convert.ToInt32(x.GetValue(null)) == System.Convert.ToInt32(value))
					.GetValue(null);
		}
	}
}
