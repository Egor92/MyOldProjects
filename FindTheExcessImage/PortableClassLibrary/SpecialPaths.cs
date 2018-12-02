using System;
using System.Collections.Generic;
using System.Text;

namespace PortableClassLibrary
{
	public static class SpecialPaths
	{
		public const string Heliohost = "http://trumpsoftware.heliohost.org/";

		public const string AppDataFolderPath = Heliohost + "findexcessimage/";

		public const string InternationalFolderPath = AppDataFolderPath + "international/";

		public const string WordsFolderPath = InternationalFolderPath + "words/";

		public const string WordsSampleListFilePath = InternationalFolderPath + "inter.words-samplelist.txt";

		public const string VersionFilePath = InternationalFolderPath + "version.txt";

		public const string LocalDiscWordsFilePath = "wordslist.xml";

		public const string LanguagesFilePath = InternationalFolderPath + "languages.txt";

		public const string ColoredImagesChanceFilePath = AppDataFolderPath + "coloredImagesChance.txt";

		public const string OldWordsFilePath = AppDataFolderPath + "words.xml";

		public static string GetWordsFilePath(string langCode)
		{
			return string.Format("{0}{1}.words.txt", WordsFolderPath, langCode);
		}

		public static Uri GetWordsUploadFileUri(string langCode)
		{
			return new Uri(string.Format("ftp://ftp.trumpsoftware.heliohost.org:21/findexcessimage/international/words/{0}.words.txt", langCode));
		}

		public static Uri VersionUploadFileUri = new Uri("ftp://ftp.trumpsoftware.heliohost.org:21/findexcessimage/international/version.txt");
	}
}
