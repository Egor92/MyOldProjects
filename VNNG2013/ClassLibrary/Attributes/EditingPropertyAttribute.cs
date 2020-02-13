using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class EditingPropertyAttribute : Attribute
	{
		private string tabItemName;
		private int tabItemNumber;

		public bool IsInitializedByString { get; set; }

		public string PropertyTranslateName { get; set; }

		public bool IsReadOnly { get; set; }

		public EditingPropertyAttribute(string propertyTranslate, string tabItemName, bool isReadOnly = false)
		{
			this.IsInitializedByString = true;
			this.tabItemName = tabItemName;
			this.PropertyTranslateName = propertyTranslate;
			this.IsReadOnly = isReadOnly;
		}

		public EditingPropertyAttribute(string propertyTranslate, int tabItemNumber = 0, bool isReadOnly = false)
		{
			this.IsInitializedByString = false;
			this.tabItemNumber = tabItemNumber;
			this.PropertyTranslateName = propertyTranslate;
			this.IsReadOnly = isReadOnly;
		}

		public EditingPropertyAttribute(string propertyTranslate, bool isEditable)
		{
			this.IsInitializedByString = false;
			this.tabItemNumber = 0;
			this.PropertyTranslateName = propertyTranslate;
			this.IsReadOnly = isEditable;
		}

		public string GetTabItemName()
		{
			return this.tabItemName;
		}

		public int GetTabItemNumber()
		{
			return this.tabItemNumber;
		}
	}
}
