using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PortableClassLibrary;

namespace WordsTranslaterApplication
{
	class Program
	{
		private static WebClient _webClient;
		public static WebClient WebClient
		{
			get
			{
				if (_webClient == null)
					_webClient = new WebClient
						{
							Credentials = new NetworkCredential("program@trumpsoftware.heliohost.org", "%RTc4diRlc3}")
						};
				return _webClient;
			}
		}

		private static string[] _sampleWords;

		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("\n***** Loading languages list... *****");
				Language.LoadLanguages();
				_sampleWords = LoadWordsSampleList();
				Console.WriteLine("***** Completed! *****");

				for (int i = 0; i < Language.AllLanguages.Count; i++)
				{
					var lang = Language.AllLanguages[i];
					Console.WriteLine("\n***** {0} language: \"{1}\" *****", i + 1, lang.Code.ToUpper());
					string dataString = GenerateDataString(lang);
					Uri fileUri = SpecialPaths.GetWordsUploadFileUri(lang.Code);
					SendFileToServer(dataString, fileUri);
				}

				UpVersion();

				Console.WriteLine("\nTotal words detected: {0}", _sampleWords.Count());
				Console.WriteLine("\n---------------------------------------------------");
				Console.WriteLine("-------   Translation has been completed   --------");
				Console.WriteLine("---------------------------------------------------");

				Console.WriteLine("\nPress any key to restart...");
				Console.ReadLine();
			}
		}

		private static void SendFileToServer(string dataString, Uri fileUri)
		{
			Console.WriteLine("\n***** Saving file to local disc... *****");
			using (var streamWriter = new StreamWriter(SpecialPaths.LocalDiscWordsFilePath))
				streamWriter.Write(dataString);
			Console.WriteLine("***** Completed! *****");

			Console.WriteLine("\n***** Sending file to server... *****");
			WebClient.UploadFile(fileUri, SpecialPaths.LocalDiscWordsFilePath);
			Console.WriteLine("***** Completed! *****");
		}

		private static string GenerateDataString(Language lang)
		{
			var result = new StringBuilder();
			for (int j = 0; j < _sampleWords.Length; j++)
			{
				string translatedWord = GoogleTranslator.Translate(_sampleWords[j], lang);
				result.Append(string.Format("{0};{1}", _sampleWords[j], translatedWord));
				Console.WriteLine("\t{0};{1}", _sampleWords[j], translatedWord);
				if (j != _sampleWords.Length - 1)
					result.AppendLine();
			}

			return result.ToString();
		}

		private static string[] LoadWordsSampleList()
		{
			return WebClient.DownloadString(SpecialPaths.WordsSampleListFilePath)
					.Split('\n')
					.Select(x => x.Trim())
					.Where(x => !string.IsNullOrWhiteSpace(x))
					.Where(x => x.First() != '#')
					.Distinct()
					.ToArray();
		}

		private static void UpVersion()
		{
			Console.WriteLine("\nVersion increase...");
			try
			{
				int version = int.Parse(WebClient.DownloadString(SpecialPaths.VersionUploadFileUri));
				WebClient.UploadString(SpecialPaths.VersionUploadFileUri, (++version).ToString());
				Console.WriteLine("***** New version number is {0} *****", version);
				Console.WriteLine("***** Completed! *****");
			}
			catch
			{
				Console.WriteLine("***** Failed to update the version on the server! *****");
			}
		}
	}
}
