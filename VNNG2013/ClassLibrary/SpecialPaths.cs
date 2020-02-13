using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClassLibrary
{
	/// <summary> Класс, содержащий пути до специальных путей </summary>
	public static class SpecialPaths
	{
		/// <summary> Путь до основной директории {USER}\My Documents\VNNG2013 </summary>
		public static string VNNG2013
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VNNG2013"; }
		}

		/// <summary> Путь до директории Data </summary>
		public static string Data
		{
			get { return SpecialPaths.VNNG2013 + "\\Data"; }
		}

		/// <summary> Путь до директории с БД </summary>
		public static string DataBases
		{
			get { return SpecialPaths.VNNG2013 + "\\DataBases"; }
		}

		/// <summary> Путь до директории с графикой </summary>
		public static string Graphics
		{
			get { return SpecialPaths.VNNG2013 + "\\Graphics"; }
		}

		/// <summary> Путь до директории с графическими изображениями лиц </summary>
		public static string Faces
		{
			get { return SpecialPaths.Graphics + "\\Faces"; }
		}

		/// <summary> Путь до директории с графическими изображениями лиц </summary>
		public static string DefaultFace
		{
			get { return "pack://application:,,,/Resources/Graphics/DefaultImages/DefaultFace.jpg"; }
		}

		/// <summary> Путь до директории с графическими изображениями стран </summary>
		public static string Countries
		{
			get { return SpecialPaths.Graphics + "\\Countries"; }
		}

		/// <summary> Путь до директории с графическими изображениями лиц </summary>
		public static string DefaultCountry
		{
			get { return "pack://application:,,,/Resources/Graphics/DefaultImages/DefaultCountry.jpg"; }
		}

		/// <summary> Проверяет наличие директории и создаёт её при необходимости </summary>
		public static void VerifyDirectory(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}
	}
}
