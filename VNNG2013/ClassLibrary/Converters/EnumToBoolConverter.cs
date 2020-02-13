using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.Windows;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(Enum), typeof(bool))]
	public class EnumToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value.ToString().Equals(parameter));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
