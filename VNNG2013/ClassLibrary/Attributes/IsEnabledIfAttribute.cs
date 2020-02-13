using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.DBClass;

namespace ClassLibrary.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class IsVisibleIfAttribute : Attribute
	{
		public string PropertyName { get; set; }

		public object[] ValueOfIsVisible { get; set; }

		public IsVisibleIfAttribute(string propertyName, params object[] valueOfIsVisible)
		{
			this.PropertyName = propertyName;
			this.ValueOfIsVisible = valueOfIsVisible;
		}
	}
}
