using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task5
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Введите исходную строку:");
			string input = Console.ReadLine();
			string result = string.Copy(input);

			Console.WriteLine("Введите слово:");
			string word = Console.ReadLine();

			Console.Write("Индексы вхождения слова в строку: ");
			while (true)
			{
				int wordIndex = result.IndexOf(word);
				if (wordIndex == -1)
					break;
				Console.Write("{0} ", wordIndex);
				result = result.Replace(word, string.Empty);
			}
			Console.WriteLine();
			Console.WriteLine("Итоговая строка:");
			Console.WriteLine(result);

			Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
			Console.Read();
		}
	}
}
