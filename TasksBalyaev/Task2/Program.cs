using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2
{
	class Program
	{
		static void Main(string[] args)
		{
			const int arrayHeight = 4;
			const int arrayWidth = 3;
			int[,] array = new int[arrayHeight, arrayWidth];
			for (int i = 0; i < arrayHeight; i++)
			{
				for (int j = 0; j < arrayWidth; j++)
				{
					int number;
					Console.WriteLine("Введите элемент [{0}, {1}]:", i, j);
					while (!int.TryParse(Console.ReadLine(), out number))
					{
						Console.WriteLine("Неверный формат! Введите число ешё раз:");
					}
					array[i, j] = number;
				}
			}
			Console.WriteLine();
			Console.WriteLine("Введённый массив:");
			for (int i = 0; i < arrayHeight; i++)
			{
				for (int j = 0; j < arrayWidth; j++)
				{
					Console.Write("{0,5}", array[i,j]);
				}
				Console.WriteLine();
			}
			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.Read();
		}
	}
}
