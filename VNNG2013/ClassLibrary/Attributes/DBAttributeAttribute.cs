using System;

namespace ClassLibrary.Attributes
{
	/// <summary>
	/// Указавает но то, что свойство соответствует столбцу в БД
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class DBAttributeAttribute : Attribute
	{
		private bool isPrimaryKey;

		private bool notNULL;

		public DBAttributeAttribute(bool notNULL = true, bool isPrimaryKey = false) 
		{
			this.notNULL = notNULL;
			this.isPrimaryKey = isPrimaryKey;
		}

		public bool IsPrimaryKey()
		{
			return this.isPrimaryKey;
		}

		public string GetFlags()
		{
			string Result = string.Empty;
			if (this.isPrimaryKey)
				return Result += " Primary Key";
			if (this.notNULL)
				return Result += " Not NULL";
			return Result;
		}
	}
}
