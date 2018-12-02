using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PortableClassLibrary;

namespace WordsTranslaterApplication
{
	class Language
	{
		public string Code;

		public Encoding Encoding;

		private static readonly List<Language> _allLanguages = new List<Language>();
		public static List<Language> AllLanguages
		{
			get { return _allLanguages; }
		}

		public static Language English
		{
			get
			{ 
				Language result = AllLanguages.SingleOrDefault(x => x.Code == "en");
				if (result == null)
					throw new Exception("English language has not been downloaded!");
				return result;
			}
		}

		private Language(string code, int encodingCode)
		{
			Code = code;
			Encoding = Encoding.GetEncoding(encodingCode);
		}

		public static Language GetLanguage(string infoString)
		{
			if (string.IsNullOrWhiteSpace(infoString))
				return null;

			infoString = infoString.Trim();

			if (infoString.Length == 0)
				return null;

			string[] langInfo = infoString.Split(':');

			if (langInfo.Length < 2)
				return null;

			string langCode = langInfo[0].Trim();

			if (!langCode.All(x => Char.IsLetter(x) || x == '-'))
				return null;

			int encodingCode;

			if (!Int32.TryParse(langInfo[1].Trim(), out encodingCode))
				return null;

			if (Encoding.GetEncodings().All(x => x.CodePage != encodingCode))
				return null;

			return new Language(langCode, encodingCode);
		}

		public static void LoadLanguages()
		{
			AllLanguages.Clear();
			using (var webClient = new WebClient())
			{
				string responcedString = webClient.DownloadString(SpecialPaths.LanguagesFilePath);
				foreach (var infoString in responcedString.Split('\n'))
				{
					var language = Language.GetLanguage(infoString);
					if (language != null)
						AllLanguages.Add(language);
				}
			}
		}
	}
}
