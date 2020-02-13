using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(bool?), typeof(string))]
	public class NullableBoolToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((value as bool?) == true) return "Основная позиция";
			else if ((value as bool?) == false) return "Второстепенная позиция";
			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return 0;
		}
	}
}
