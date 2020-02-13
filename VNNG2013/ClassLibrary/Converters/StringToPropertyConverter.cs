using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.ComponentModel;

namespace ClassLibrary.Converters
{
	public class StringToPropertyConverter : TypeConverter
	{
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string[] str = (value as string).Split('.');
			Type t = Assembly.GetExecutingAssembly().GetTypes().Single(x => x.Name == str[0]);
			PropertyInfo pi = t.GetProperty(str[1]);
			return pi;
		}
	}
}
