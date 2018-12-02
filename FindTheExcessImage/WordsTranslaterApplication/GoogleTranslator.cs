using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ude; 

namespace WordsTranslaterApplication
{
	static class GoogleTranslator
	{
		private static readonly Regex SelectTranslatedByGoogleWord = new Regex(@"{"".*?""trans"":""(?<result>.*?)""");

		private static readonly WebClient ClientforTranslate = new WebClient();

		public static string Translate(string word, Language toLang)
		{
			if (string.IsNullOrWhiteSpace(word)) 
				return "";
			if (toLang.Code == "en") 
				return word;

			byte[] bytesText = ClientforTranslate.DownloadData(
				string.Format("http://translate.google.com/translate_a/t?client=x&text={0}&sl=en&tl={1}", word, toLang.Code));

            string text = toLang.Encoding.GetString(bytesText);
            int current = 0;
            while ((current = text.IndexOf(@"\u")) != -1)
            {
                int charCode = Convert.ToInt32("0x" + text.Substring(current + 2, 4), 16);
                text = text.Replace(text.Substring(current, 6), char.ConvertFromUtf32(charCode));
            }
			return SelectTranslatedByGoogleWord.Match(text).Groups["result"].ToString();
		}
	}
}
