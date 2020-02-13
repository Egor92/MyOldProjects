using System;
using System.Linq;
using ClassLibrary.DBClass;
using System.Reflection;
using System.Collections.Generic;

namespace ClassLibrary.Attributes
{
	public enum MemberType
	{
		Property, Method
	}


	/// <summary> Условия на возможные значения </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ConditionAttribute : Attribute
	{
		public Func<DBObject, bool> Condition { get; set; }

		public ConditionAttribute(MemberType memberType, string memberName) 
		{
			if (memberType == MemberType.Method)
				Condition = new Func<DBObject, bool>(x => (bool)x.GetType().GetMethod(memberName).Invoke(x, null));
			else if (memberType == MemberType.Property)
				Condition = new Func<DBObject, bool>(x => (bool)x.GetType().GetProperty(memberName).GetValue(x, null));
			else
				throw new NotImplementedException();
		}

		public static List<Func<DBObject, bool>> GetConditions(PropertyInfo property)
		{
			IEnumerable<ConditionAttribute> conditionAttributes = property.GetCustomAttributes(typeof(ConditionAttribute), false).Cast<ConditionAttribute>();
			List<Func<DBObject, bool>> Result = new List<Func<DBObject,bool>>();
			foreach (ConditionAttribute conditionAttribute in conditionAttributes)
				Result.Add(conditionAttribute.Condition);
			return Result;
		}

	}
}
