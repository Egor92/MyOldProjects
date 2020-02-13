using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using ClassLibrary.DBClass;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(IEnumerable), typeof(bool))]
	public class TacticItemsSourceToEnabledConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value as IEnumerable<Tactic>).Count() > 1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
