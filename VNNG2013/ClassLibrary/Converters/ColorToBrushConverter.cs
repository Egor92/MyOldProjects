using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.Windows;

namespace ClassLibrary.Converters
{
	[ValueConversion(typeof(Color), typeof(SolidColorBrush))]
	public class ColorToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			LinearGradientBrush brush = new LinearGradientBrush();
			brush.StartPoint = new Point(1.0, 0.0);
			brush.EndPoint = new Point(0.0, 1.0);
			brush.GradientStops.Add(new GradientStop((Color)value, 0.0));
			Color endColor = ((Color)value).Multiply(1.2);
			brush.GradientStops.Add(new GradientStop(endColor, 0.6));
			brush.GradientStops.Add(new GradientStop((Color)value, 1.0));
			return brush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((SolidColorBrush)value).Color;
		}
	}
}
