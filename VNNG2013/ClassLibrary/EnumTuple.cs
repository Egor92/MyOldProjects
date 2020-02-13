using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
	public class EnumTuple : Tuple<int,string>
	{
		public EnumTuple(int item1, string item2) : base(item1, item2) { }

		public override string ToString()
		{
			return this.Item2;
		}
	}
}
