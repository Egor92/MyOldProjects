using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
	class Program
	{
		static void Main(string[] args)
		{
			int k;
			while (true)
			{
				Console.WriteLine("Введите параметр 'k':");
				while (!int.TryParse(Console.ReadLine(), out k))
				{
					Console.WriteLine("Неверный формат! Введите число ешё раз:");
				}
				if (k >= 1 && k <= 10)
					break;
				Console.WriteLine("Параметр 'k' должен быть целым числом и удовлетворять условию (1 ≤ k ≤ 10)");
			}

			double alfa;
			Console.WriteLine("Введите параметр 'alfa':");
			while (!double.TryParse(Console.ReadLine(), out alfa))
			{
				Console.WriteLine("Неверный формат! Введите число ешё раз:");
			}

			Console.WriteLine();
			Console.WriteLine("Матрица:");
			for (int i = 0; i < k; i++)
			{
				for (int j = 0; j < k; j++)
				{
					Console.Write("{0,7}", Func(i, j, k, alfa).ToString("F2"));
				}
				Console.WriteLine();
			}
			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.Read();
		}

		private static double Func(int i, int j, int k, double alfa)
		{
			if (i <= j)
				return F(k-(j-i), alfa);
			else
				return 1.0;
		}

		private static double F(int power, double alfa)
		{
			return Math.Pow(Math.Cos(alfa), power);
		}
	}
}
