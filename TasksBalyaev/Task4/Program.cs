using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task4
{
	class Program
	{
		static void Main(string[] args)
		{
			double a;
			Console.WriteLine("Введите параметр 'a':");
			while (!double.TryParse(Console.ReadLine(), out a))
			{
				Console.WriteLine("Неверный формат! Введите число ешё раз:");
			}

			double b;
			Console.WriteLine("Введите параметр 'b':");
			while (true)
			{
				while (!double.TryParse(Console.ReadLine(), out b))
				{
					Console.WriteLine("Неверный формат! Введите число ешё раз:");
				}
				if (b > a)
					break;
				Console.WriteLine("Параметр 'b' должен быть больше параметра 'a'");
			}

			double c;
			Console.WriteLine("Введите параметр 'c':");
			while (!double.TryParse(Console.ReadLine(), out c))
			{
				Console.WriteLine("Неверный формат! Введите число ешё раз:");
			}

			double d;
			Console.WriteLine("Введите параметр 'd':");
			while (true)
			{
				while (!double.TryParse(Console.ReadLine(), out d))
				{
					Console.WriteLine("Неверный формат! Введите число ешё раз:");
				}
				if (d > c)
					break;
				Console.WriteLine("Параметр 'd' должен быть больше параметра 'c'");
			}

			double h_x;
			Console.WriteLine("Введите параметр 'h_x':");
			while (true)
			{
				while (!double.TryParse(Console.ReadLine(), out h_x))
				{
					Console.WriteLine("Неверный формат! Введите число ешё раз:");
				}
				if (h_x > 0)
					break;
				Console.WriteLine("Параметр 'h_x' должен быть положительным!");
			}

			double h_y;
			Console.WriteLine("Введите параметр 'h_y':");
			while (true)
			{
				while (!double.TryParse(Console.ReadLine(), out h_y))
				{
					Console.WriteLine("Неверный формат! Введите число ешё раз:");
				}
				if (h_y > 0)
					break;
				Console.WriteLine("Параметр 'h_y' должен быть положительным!");
			}

			Console.WriteLine("Матрица:");
			double x = a;
			double sum = 0.0;
			while (x < b)
			{
				double y = c;
				while (y < d)
				{
					double value = F(x, y);
					Console.Write("{0,7}", value.ToString("F2"));
					sum += value;
					y += h_y;
				}
				Console.WriteLine();
				x += h_x;
			}
			Console.WriteLine("Сумма элементов: {0}", sum);

			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.Read();
		}

		private static double F(double x, double y)
		{
			if (x < y)
				return Math.Pow(1 + Math.Abs(y), 2 * x) + x;
			if (x == y)
				return Math.Pow(y*y, 1/3)*Math.Atan(x + y);
			return Math.Tan(x + y);
		}
	}
}
