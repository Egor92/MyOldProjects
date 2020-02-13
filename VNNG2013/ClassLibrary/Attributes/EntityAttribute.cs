using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.DBClass;

namespace ClassLibrary.Attributes
{
	/// <summary> Указывает на то, что данный класс является описанием соответствующей сущности Базы Данных </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class EntityAttribute : Attribute
	{
		/// <summary> Указывает на то, является ли данный объект независимым </summary>
		private bool isIndependent;
		public bool IsIndependent
		{
			get { return this.isIndependent; }
		}

		private string translationName;
		public string TranslationName
		{
			get { return this.translationName; }
		}

		private Type firstType;
		public Type FirstType 
		{
			get { return this.firstType; }
		}

		private Type secondType;
		public Type SecondType 
		{
			get { return this.secondType; }
		}

		private bool isManyToMany;
		public bool IsManyToMany 
		{
			get { return this.isManyToMany; }
		}

		public EntityAttribute(Type firstType, Type secondType)
		{
			this.isManyToMany = true;

			if (firstType == secondType)
				throw new ArgumentException("Нарочно не поддерживается возможность задания связи \"Многие ко многим\" между одной и той же сущностью");

			if (firstType == null)
				throw new ArgumentNullException("Параметр firstType не должен иметь значение null");
			if (!firstType.Is<DBObject>())
				throw new ArgumentNullException("Параметр firstType должен наследоваться от DBObject");
			if (!firstType.HasAttribute<EntityAttribute>())
				throw new ArgumentNullException("Параметр firstType должен иметь атрибут EntityAttribute");

			if (secondType == null)
				throw new ArgumentNullException("Параметр secondType не должен иметь значение null");
			if (!secondType.Is<DBObject>())
				throw new ArgumentNullException("Параметр secondType должен наследоваться от DBObject");
			if (!secondType.HasAttribute<EntityAttribute>())
				throw new ArgumentNullException("Параметр secondType должен иметь атрибут EntityAttribute");

			this.firstType = firstType;
			this.secondType = secondType;
		}

		public EntityAttribute(string translationName, bool isIndependent) 
		{
			this.isManyToMany = false;
			this.translationName = translationName;
			this.isIndependent = isIndependent;
		}

		/// <summary> Проверяет соответствие указанных типов типам, указанным в конструкторе </summary>
		public bool HasTypes(Type first, Type second)
		{
			if (this.IsManyToMany &&
				(first == this.FirstType && second == this.SecondType ||
				first == this.SecondType && second == this.FirstType))
				{
					return true;
				}
			return false;
		}

	}
}
