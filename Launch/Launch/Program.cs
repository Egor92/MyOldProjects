using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Launch
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Электронный учебно-методический комплекс");
			Console.WriteLine("Военно-техническая подготовка");
			Console.WriteLine();
			Console.WriteLine("Загрузка...");
			Console.WriteLine();
			string appPath = System.IO.Path.GetFullPath(@"EUMK\bin\Release\EUMK.exe");
			string folderPath = System.IO.Path.GetDirectoryName(appPath);
			try
			{
				Process.Start(new ProcessStartInfo(appPath) { WorkingDirectory = folderPath });
			}
			catch (Exception)
			{
				Console.WriteLine("Файл {0} не найден!", appPath);
				Console.ReadKey();
			}
		}
	}
}
