using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.Windows;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(int), typeof(bool))]
	public class SelectedIndexToEnabledConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((int)value) != -1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
