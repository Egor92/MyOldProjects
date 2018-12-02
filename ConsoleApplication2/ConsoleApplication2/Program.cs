//#define Watching

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
	class Program
	{
		static void Main(string[] args)
		{
			A(new int[2]);
			Console.Read();
		}

		[Conditional("Watching")]
		private static void A(Array array)
		{
			Console.WriteLine(array.GetType().GetElementType());
		}
	}
}
