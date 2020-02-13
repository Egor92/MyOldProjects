using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class FieldTranslationAttribute : Attribute
	{
		public string FieldTranslateName { get; set; }

		public FieldTranslationAttribute(string fieldTranslateName)
		{
			this.FieldTranslateName = fieldTranslateName;
		}
	}
}
