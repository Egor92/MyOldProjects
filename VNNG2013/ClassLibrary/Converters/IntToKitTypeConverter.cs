using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(int), typeof(string))]
	public class IntToKitTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((int)value)
			{
				case 0:
					return "Форма вратаря";
				case 1:
					return "Домашний комплект";
				case 2:
					return "Гостевой комплект";
				default:
					return string.Format("{0}-й резервный комплект", (int)value);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
