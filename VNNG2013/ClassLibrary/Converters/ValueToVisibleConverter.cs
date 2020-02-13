using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(object), typeof(bool))]
	public class ValueToVisibleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			foreach (object obj in parameter as object[])
				if (value.Equals(obj))
					return Visibility.Visible;
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return 0;
		}
	}
}
