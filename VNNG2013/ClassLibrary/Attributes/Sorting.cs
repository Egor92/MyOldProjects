using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ClassLibrary.Attributes
{
	public class SortingAttribute : Attribute
	{
		private string memberName;
		public string MemberName
		{
			get { return this.memberName; }
		}

		public SortingAttribute(string memberName)
		{
			this.memberName = memberName;
		}

		public MemberInfo GetMemberInfo(Type type)
		{
			MemberInfo Result = type.GetMember(this.MemberName, MemberTypes.Property | MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
			if (Result == null)
				throw new Exception(string.Format("У типа '{0}' не найден метод или свойство с именем '{1}'", type, this.MemberName));
			return Result;
		}
	}
}
