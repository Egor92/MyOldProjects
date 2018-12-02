using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
	class Program
	{
		static void Main(string[] args)
		{
			double x, a;
			Console.WriteLine("Введите параметр 'x':");
			while (!double.TryParse(Console.ReadLine(), out x))
			{
				Console.WriteLine("Неверный формат! Введите число ешё раз:");
			}
			Console.WriteLine("Введите параметр 'a':");
			while (!double.TryParse(Console.ReadLine(), out a))
			{
				Console.WriteLine("Неверный формат! Введите число ешё раз:");
			}
			Console.WriteLine("Y({0}, {1})={2}", x ,a, Y(x, a));
			Console.WriteLine("Z({0}, {1})={2}", x, a, Z(x, a));
			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.Read();
		}

		private static double Y(double x, double a)
		{
			return Math.Log(Math.E, 1.5 * x) + a * a * a * a;
		}

		private static double Z(double x, double a)
		{
			return Math.Atan(Math.Cos(a/(x*x)));
		}
	}
}
