using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using ClassLibrary.DBClass;
using ClassLibrary;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DataGrabber
{
	class Program
	{
		static void Main(string[] args)
		{
			return;


			DataGrabber grabber = new DataGrabber();

			foreach (Uri link in grabber.GetClubsLinks("http://www.transfermarkt.de/en/1-liga-russland/startseite/wettbewerb_RU1.html"))
			{
				Club club = grabber.GetClub(link);
				//Console.WriteLine("------------------------------------------------------");
				//Console.WriteLine("-> Клуб: {0}", club);
				//Console.WriteLine("-> Менеджер: {0}", club.Manager);
				foreach (Person player in club.Players)
				{
					//Console.WriteLine("---> {0} ({1})", player.DisplayedText, player.Nation);
				}
			}
			Console.ReadLine();
		}

	}

	class DataGrabber
	{
		WebClient client = new WebClient() { Encoding = Encoding.UTF8 };

		DataStorage storage = new DataStorage(SpecialPaths.DataBases + "//123.sqlite");

		Dictionary<string, Nation> nations = new Dictionary<string, Nation>();

		Dictionary<string, City> cities = new Dictionary<string, City>();

		private Uri CompleteUrl(string url)
		{
			return new Uri("http://www.transfermarkt.de" + url);
		}

		static Regex selectClubsTable = new Regex(@"<table id=""vereine""(\s|.)*?</table>");
		static Regex selectLinksInClubsTable = new Regex(@"<td class=""ac wid10"">\s*<a href=""(?<link>(\s|.)*?)""");
		public List<Uri> GetClubsLinks(string championshipUrl)
		{
			string text = client.DownloadString(championshipUrl);

			Match table = selectClubsTable.Match(text); //Выбрать таблицу
			MatchCollection matches = selectLinksInClubsTable.Matches(text); //Выбрать ссылки на клубы

			List<Uri> result = new List<Uri>();

			foreach (Match match in matches)
			{
				result.Add(CompleteUrl(match.Groups["link"].Value));
			}

			return result;
		}

		static Regex selectClubInfo = new Regex(@"<td class=""blau vm verein_header_table_gross_bg"">(\s|.)*?<a.*?>(?<name>.*?)<(\s|.)*?<a.*?>(?<country>.*?)<");
		static Regex selectManagerUrl = new Regex(@"<div id=""bxTrainer"">(.|\s)*?Name: <a href=""(?<url>(.|\s)*?)""");
		static Regex selectPlayersTable = new Regex(@"<table id=""spieler""(\s|.)*?</table>\s*<p class=""drunter ar"">");
		static Regex selectLinksToPlayers = new Regex(@"<td class=""s10"">\s*<a href=""(?<link>(\s|.)*?)""");
		public Club GetClub(Uri clubUrl)
		{
			string text = client.DownloadString(clubUrl);
			Match match = selectClubInfo.Match(text);
			Club result = (Club)storage.CreateDBObject(typeof(Club));
			string name = match.Groups["name"].ToString();
			result.Name = Translate(name);
			result.City = GetCity(name.Split(' ').Last(), GetNation(match.Groups["country"].ToString()));
			Console.WriteLine("------------------------------------------------------");
			Console.WriteLine("-> Клуб: {0}", result);
			result.Manager = GetManager(CompleteUrl(selectManagerUrl.Match(text).Groups["url"].ToString()));

			string table = selectPlayersTable.Match(text).ToString();
			MatchCollection playersLinks = selectLinksToPlayers.Matches(table);

			List<Person> players = new List<Person>();

			foreach (Match link in playersLinks)
			{
				players.Add(GetPlayer(CompleteUrl(link.Groups["link"].ToString())));
			}
			result.Players = players.ToArray();

			return result;
		}

		static Regex selectPlayerInfo = new Regex(@"<table class=""tabelle_spieler s10"".*?>\s*(?:<tr>\s*<td.*?>Name in native country:</td>\s*<td class=""fn n"">\s*<span class=""given-name s10"">(?<surname>(?:[а-я]|[А-Я])*) (?<name>(?:[а-я]|[А-Я])*) .*?</span>)?(?:.|\s)*?Date of birth:</td>\s*<td><a.*?>(?<date>\d{2}.\d{2}.\d{4})</a>(?:.|\s)*?Height:</td>\s*<td>(?<height>.*?)</td>(?:.|\s)*?<span class=""country-name s10"">(?<country>.*?)</span>(?:.|\s)*?<td class=""class"">(?<position>.*?)</td>(?:.|\s)*?(?:class=""note"">(?<cost>.*?) &euro;|<td>-</td>)");
		static Regex selectPlayerName = new Regex(@"<title>(?<name>.*?) (?<surname>.*?)-");
		private Person GetPlayer(Uri playerUrl)
		{
			Person result = (Person)storage.CreateDBObject(typeof(Person));

			string text = client.DownloadString(playerUrl);
			Match match = selectPlayerInfo.Match(text);

			if (!string.IsNullOrWhiteSpace(match.Groups["name"].ToString())) //Если есть имя и фамилия на русском
			{
				result.Surname = match.Groups["surname"].ToString();
				result.Name = match.Groups["name"].ToString();
			}
			else
			{
				Match nameMatch = selectPlayerName.Match(text);
				result.Surname = Translate(nameMatch.Groups["surname"].ToString());
				result.Name = Translate(nameMatch.Groups["name"].ToString());
			}
			result.Type = Person.PersonType.Player;
			result.BirthdayDate = DateTime.Parse(match.Groups["date"].ToString());
			result.Nation = GetNation(match.Groups["country"].ToString());
			//result.Height = double.Parse(match.Groups["height"].ToString()); //TODO
			Console.WriteLine("---> {0} ({1})", result.DisplayedText, result.Nation);
			return result;
		}

		static Regex selectManagerInfo = new Regex(@"<table class=""tabelle_spieler s10"".*?>\s*(?:<tr>\s*<td.*?>Name in native country:</td>\s*<td class=""fn n"">\s*<span class=""given-name s10"">(?<surname>(?:[а-я]|[А-Я])*) (?<name>(?:[а-я]|[А-Я])*) .*?</span>)?(?:.|\s)*?Date of birth:</td>\s*<td class=""s10"">(?<date>\d{2}.\d{2}.\d{4})</td>(?:.|\s)*?<span class=""country-name s10"">(?<country>\w*)<(?:.|\s)*?Preferred formation: (?<formation>.*?)""");
		static Regex selectManagerName = new Regex(@"<title>(?<name>.*?) (?<surname>.*?) -");
		public Person GetManager(Uri managerUrl)
		{
			Person result = (Person)storage.CreateDBObject(typeof(Person));

			string text = client.DownloadString(managerUrl);
			Match match = selectManagerInfo.Match(text);
			result.Type = Person.PersonType.Manager;
			if (!string.IsNullOrWhiteSpace(match.Groups["surname"].ToString())) //Если есть имя и фамилия на русском
			{
				result.Surname = match.Groups["surname"].ToString();
				result.Name = match.Groups["name"].ToString();
			}
			else
			{
				Match nameMatch = selectManagerName.Match(text);
				result.Surname = Translate(nameMatch.Groups["surname"].ToString());
				result.Name = Translate(nameMatch.Groups["name"].ToString());
			}
			result.BirthdayDate = DateTime.Parse(match.Groups["date"].ToString());
			result.Nation = GetNation(match.Groups["country"].ToString());
			Console.WriteLine("-> Менеджер: {0}", result);
			return result;
		}

		private Nation GetNation(string name)
		{
			if (nations.ContainsKey(name))
			{
				return nations[name];
			}
			else
			{
				Nation result = (Nation)storage.CreateDBObject(typeof(Nation));
				result.Name = Translate(name);
				//result.Name = name;
				nations.Add(name, result);
				return result;
			}
		}


		private City GetCity(string name, Nation nation)
		{
			if (cities.ContainsKey(name))
			{
				return cities[name];
			}
			else
			{
				City result = (City)storage.CreateDBObject(typeof(City));
				result.Nation = nation;
				result.Name = Translate(name);
				cities.Add(name, result);
				return result;
			}
		}


		static Regex selectTranslatedByYandexWord = new Regex(@"<text>(?<result>.*?)</text>");
		string TranslateByYandex(string word)
		{
			if (string.IsNullOrWhiteSpace(word)) return "";
			/*string text = client.DownloadString(string.Format("http://translate.yandex.net/api/v1/tr/translate?lang={0}&text={1}", "ru", word));
			return selectTranslatedByYandexWord.Match(text).Groups["result"].ToString();*/

			XmlDocument doc = new XmlDocument();
			doc.Load(client.OpenRead(string.Format("http://translate.yandex.net/api/v1/tr/translate?lang={0}&text={1}", "ru", word)));
			XmlNode node = doc.SelectSingleNode("/Translation/text[1]");
			return node.InnerText;
		}

		static Regex selectTranslatedByGoogleWord = new Regex(@"{"".*?""trans"":""(?<result>.*?)""");
		WebClient clientforTranslate = new WebClient();
		string TranslateByGoogle(string word)
		{
			if (string.IsNullOrWhiteSpace(word)) return "";

			string text = clientforTranslate.DownloadString(string.Format("http://translate.google.ru/translate_a/t?client=x&text={0}&sl={1}&tl={2}", word, "deu", "rus"));
			return selectTranslatedByGoogleWord.Match(text).Groups["result"].ToString();
		}

		string Translate(string word)
		{
			return this.TranslateByYandex(word);
		}

	}
}