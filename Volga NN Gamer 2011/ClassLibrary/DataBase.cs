using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Drawing;
using System.Reflection;
using VNNG2011;


namespace VNNG2011
{
	public static class DataBase
	{

        #region Обьявление таблиц
        internal static db.AbilityPriorityDataTable TableOfAbilityPriority;
        internal static db.AmpluaDataTable TableOfAmplua;
        internal static db.AttributesDataTable TableOfAttributes;
        internal static db.ChampionshipDataTable TableOfChampionship;
        internal static db.CityDataTable TableOfCity;
        internal static db.ClubDataTable TableOfClub;
        internal static db.ClubPopularityDataTable TableOfClubPopularity;
        internal static db.CoachInfoDataTable TableOfCoachInfo;
        internal static db.CountryDataTable TableOfCountry;
        internal static db.GameDataDataTable TableOfGameData;
        internal static db.HistoryOfClubDataTable TableOfHistoryOfClub;
        internal static db.HistoryOfPlayerDataTable TableOfHistoryOfPlayer;
        internal static db.HistoryOfChampionshipDataTable TableOfHistoryOfChampionship;
        internal static db.InjuryDataTable TableOfInjury;
        internal static db.InterestDataTable TableOfInterest;
        internal static db.MatchOfClubsDataTable TableOfMatchOfClubs;
        internal static db.MatchOfCountriesDataTable TableOfMatchOfCountries;
        internal static db.PersonDataTable TableOfPerson;
        internal static db.PlayedMatchesOfPlayerDataTable TableOfPlayedMatchesOfPlayer;
        internal static db.PlayerInfoDataTable TableOfPlayerInfo;
        internal static db.PriceValueDataTable TableOfPriceValue;
        internal static db.RefereeInfoDataTable TableOfRefereeInfo;
        internal static db.RegionDataTable TableOfRegion;
        internal static db.StadiumDataTable TableOfStadium;
        internal static db.TacticOfLineDataTable TableOfTacticOfLine;
        internal static db.TacticPointDataTable TableOfTacticPoint;
        internal static db.TacticPointLocationDataTable TableOfTacticPointLocation;
        internal static db.TotalTacticDataTable TableOfTotalTactic;
        internal static db.TrainingDataTable TableOfTraining;
        internal static db.TransferDataTable TableOfTransfer;
        internal static db.TypeOfChampDataTable TableOfTypeOfChamp;
        internal static db.TypeOfInjuryDataTable TableOfTypeOfInjury;
        internal static db.KitColorsOfClubDataTable TableOfKitColorsOfClub;
        internal static db.KitColorsOfCountryDataTable TableOfKitColorsOfCountry;
        internal static db.ColorDataTable TableOfColor;
        internal static db.ManagerInfoDataTable TableOfManagerInfo;
        internal static db.ClubsOfChampionshipDataTable TableOfClubsOfChampionship;
        internal static db.SquadOfClubDataTable TableOfSquadOfClub;
        #endregion

        #region Обьявление адаптеров
        static dbTableAdapters.AbilityPriorityTableAdapter AdapterOfAbilityPriority;
        static dbTableAdapters.AmpluaTableAdapter AdapterOfAmplua;
        static dbTableAdapters.AttributesTableAdapter AdapterOfAttributes;
        static dbTableAdapters.ChampionshipTableAdapter AdapterOfChampionship;
        static dbTableAdapters.CityTableAdapter AdapterOfCity;
        static dbTableAdapters.ClubTableAdapter AdapterOfClub;
        static dbTableAdapters.ClubPopularityTableAdapter AdapterOfClubPopularity;
        static dbTableAdapters.CoachInfoTableAdapter AdapterOfCoachInfo;
        static dbTableAdapters.CountryTableAdapter AdapterOfCountry;
        static dbTableAdapters.GameDataTableAdapter AdapterOfGameData;
        static dbTableAdapters.HistoryOfClubTableAdapter AdapterOfHistoryOfClub;
        static dbTableAdapters.HistoryOfPlayerTableAdapter AdapterOfHistoryOfPlayer;
        static dbTableAdapters.HistoryOfChampionshipTableAdapter AdapterOfHistoryOfChampionship;
        static dbTableAdapters.InjuryTableAdapter AdapterOfInjury;
        static dbTableAdapters.InterestTableAdapter AdapterOfInterest;
        static dbTableAdapters.MatchOfClubsTableAdapter AdapterOfMatchOfClubs;
        static dbTableAdapters.MatchOfCountriesTableAdapter AdapterOfMatchOfCountries;
        static dbTableAdapters.PersonTableAdapter AdapterOfPerson;
        static dbTableAdapters.PlayedMatchesOfPlayerTableAdapter AdapterOfPlayedMatchesOfPlayer;
        static dbTableAdapters.PlayerInfoTableAdapter AdapterOfPlayerInfo;
        static dbTableAdapters.PriceValueTableAdapter AdapterOfPriceValue;
        static dbTableAdapters.RefereeInfoTableAdapter AdapterOfRefereeInfo;
        static dbTableAdapters.RegionTableAdapter AdapterOfRegion;
        static dbTableAdapters.StadiumTableAdapter AdapterOfStadium;
        static dbTableAdapters.TacticOfLineTableAdapter AdapterOfTacticOfLine;
        static dbTableAdapters.TacticPointTableAdapter AdapterOfTacticPoint;
        static dbTableAdapters.TacticPointLocationTableAdapter AdapterOfTacticPointLocation;
        static dbTableAdapters.TotalTacticTableAdapter AdapterOfTotalTactic;
        static dbTableAdapters.TrainingTableAdapter AdapterOfTraining;
        static dbTableAdapters.TransferTableAdapter AdapterOfTransfer;
        static dbTableAdapters.TypeOfChampTableAdapter AdapterOfTypeOfChamp;
        static dbTableAdapters.TypeOfInjuryTableAdapter AdapterOfTypeOfInjury;
        static dbTableAdapters.KitColorsOfClubTableAdapter AdapterOfKitColorsOfClub;
        static dbTableAdapters.KitColorsOfCountryTableAdapter AdapterOfKitColorsOfCountry;
        static dbTableAdapters.ColorTableAdapter AdapterOfColor;
        static dbTableAdapters.ManagerInfoTableAdapter AdapterOfManagerInfo;
        static dbTableAdapters.ClubsOfChampionshipTableAdapter AdapterOfClubsOfChampionship;
        static dbTableAdapters.SquadOfClubTableAdapter AdapterOfSquadOfClub;
        #endregion

        static DataBase()
        {
            AdapterOfAbilityPriority = new dbTableAdapters.AbilityPriorityTableAdapter();
            TableOfAbilityPriority = new db.AbilityPriorityDataTable();
            AdapterOfAbilityPriority.Fill(TableOfAbilityPriority);

            AdapterOfAmplua = new dbTableAdapters.AmpluaTableAdapter();
            TableOfAmplua = new db.AmpluaDataTable();
            AdapterOfAmplua.Fill(TableOfAmplua);

            AdapterOfAttributes = new dbTableAdapters.AttributesTableAdapter();
            TableOfAttributes = new db.AttributesDataTable();
            AdapterOfAttributes.Fill(TableOfAttributes);

            AdapterOfChampionship = new dbTableAdapters.ChampionshipTableAdapter();
            TableOfChampionship = new db.ChampionshipDataTable();
            AdapterOfChampionship.Fill(TableOfChampionship);

            AdapterOfCity = new dbTableAdapters.CityTableAdapter();
            TableOfCity = new db.CityDataTable();
            AdapterOfCity.Fill(TableOfCity);

            AdapterOfClub = new dbTableAdapters.ClubTableAdapter();
            TableOfClub = new db.ClubDataTable();
            AdapterOfClub.Fill(TableOfClub);

            AdapterOfClubPopularity = new dbTableAdapters.ClubPopularityTableAdapter();
            TableOfClubPopularity = new db.ClubPopularityDataTable();
            AdapterOfClubPopularity.Fill(TableOfClubPopularity);

            AdapterOfCoachInfo = new dbTableAdapters.CoachInfoTableAdapter();
            TableOfCoachInfo = new db.CoachInfoDataTable();
            AdapterOfCoachInfo.Fill(TableOfCoachInfo);

            AdapterOfCountry = new dbTableAdapters.CountryTableAdapter();
            TableOfCountry = new db.CountryDataTable();
            AdapterOfCountry.Fill(TableOfCountry);

            AdapterOfGameData = new dbTableAdapters.GameDataTableAdapter();
            TableOfGameData = new db.GameDataDataTable();
            AdapterOfGameData.Fill(TableOfGameData);

            AdapterOfHistoryOfClub = new dbTableAdapters.HistoryOfClubTableAdapter();
            TableOfHistoryOfClub = new db.HistoryOfClubDataTable();
            AdapterOfHistoryOfClub.Fill(TableOfHistoryOfClub);

            AdapterOfHistoryOfPlayer = new dbTableAdapters.HistoryOfPlayerTableAdapter();
            TableOfHistoryOfPlayer = new db.HistoryOfPlayerDataTable();
            AdapterOfHistoryOfPlayer.Fill(TableOfHistoryOfPlayer);

            AdapterOfHistoryOfChampionship = new dbTableAdapters.HistoryOfChampionshipTableAdapter();
            TableOfHistoryOfChampionship = new db.HistoryOfChampionshipDataTable();
            AdapterOfHistoryOfChampionship.Fill(TableOfHistoryOfChampionship);

            AdapterOfInjury = new dbTableAdapters.InjuryTableAdapter();
            TableOfInjury = new db.InjuryDataTable();
            AdapterOfInjury.Fill(TableOfInjury);

            AdapterOfInterest = new dbTableAdapters.InterestTableAdapter();
            TableOfInterest = new db.InterestDataTable();
            AdapterOfInterest.Fill(TableOfInterest);

            AdapterOfMatchOfClubs = new dbTableAdapters.MatchOfClubsTableAdapter();
            TableOfMatchOfClubs = new db.MatchOfClubsDataTable();
            AdapterOfMatchOfClubs.Fill(TableOfMatchOfClubs);

            AdapterOfMatchOfCountries = new dbTableAdapters.MatchOfCountriesTableAdapter();
            TableOfMatchOfCountries = new db.MatchOfCountriesDataTable();
            AdapterOfMatchOfCountries.Fill(TableOfMatchOfCountries);

            AdapterOfPerson = new dbTableAdapters.PersonTableAdapter();
            TableOfPerson = new db.PersonDataTable();
            AdapterOfPerson.Fill(TableOfPerson);

            AdapterOfPlayedMatchesOfPlayer = new dbTableAdapters.PlayedMatchesOfPlayerTableAdapter();
            TableOfPlayedMatchesOfPlayer = new db.PlayedMatchesOfPlayerDataTable();
            AdapterOfPlayedMatchesOfPlayer.Fill(TableOfPlayedMatchesOfPlayer);

            AdapterOfPlayerInfo = new dbTableAdapters.PlayerInfoTableAdapter();
            TableOfPlayerInfo = new db.PlayerInfoDataTable();
            AdapterOfPlayerInfo.Fill(TableOfPlayerInfo);

            AdapterOfPriceValue = new dbTableAdapters.PriceValueTableAdapter();
            TableOfPriceValue = new db.PriceValueDataTable();
            AdapterOfPriceValue.Fill(TableOfPriceValue);

            AdapterOfRefereeInfo = new dbTableAdapters.RefereeInfoTableAdapter();
            TableOfRefereeInfo = new db.RefereeInfoDataTable();
            AdapterOfRefereeInfo.Fill(TableOfRefereeInfo);

            AdapterOfRegion = new dbTableAdapters.RegionTableAdapter();
            TableOfRegion = new db.RegionDataTable();
            AdapterOfRegion.Fill(TableOfRegion);

            AdapterOfStadium = new dbTableAdapters.StadiumTableAdapter();
            TableOfStadium = new db.StadiumDataTable();
            AdapterOfStadium.Fill(TableOfStadium);

            AdapterOfTacticOfLine = new dbTableAdapters.TacticOfLineTableAdapter();
            TableOfTacticOfLine = new db.TacticOfLineDataTable();
            AdapterOfTacticOfLine.Fill(TableOfTacticOfLine);

            AdapterOfTacticPoint = new dbTableAdapters.TacticPointTableAdapter();
            TableOfTacticPoint = new db.TacticPointDataTable();
            AdapterOfTacticPoint.Fill(TableOfTacticPoint);

            AdapterOfTacticPointLocation = new dbTableAdapters.TacticPointLocationTableAdapter();
            TableOfTacticPointLocation = new db.TacticPointLocationDataTable();
            AdapterOfTacticPointLocation.Fill(TableOfTacticPointLocation);

            AdapterOfTotalTactic = new dbTableAdapters.TotalTacticTableAdapter();
            TableOfTotalTactic = new db.TotalTacticDataTable();
            AdapterOfTotalTactic.Fill(TableOfTotalTactic);

            AdapterOfTraining = new dbTableAdapters.TrainingTableAdapter();
            TableOfTraining = new db.TrainingDataTable();
            AdapterOfTraining.Fill(TableOfTraining);

            AdapterOfTransfer = new dbTableAdapters.TransferTableAdapter();
            TableOfTransfer = new db.TransferDataTable();
            AdapterOfTransfer.Fill(TableOfTransfer);

            AdapterOfTypeOfChamp = new dbTableAdapters.TypeOfChampTableAdapter();
            TableOfTypeOfChamp = new db.TypeOfChampDataTable();
            AdapterOfTypeOfChamp.Fill(TableOfTypeOfChamp);

            AdapterOfTypeOfInjury = new dbTableAdapters.TypeOfInjuryTableAdapter();
            TableOfTypeOfInjury = new db.TypeOfInjuryDataTable();
            AdapterOfTypeOfInjury.Fill(TableOfTypeOfInjury);

            AdapterOfKitColorsOfClub = new dbTableAdapters.KitColorsOfClubTableAdapter();
            TableOfKitColorsOfClub = new db.KitColorsOfClubDataTable();
            AdapterOfKitColorsOfClub.Fill(TableOfKitColorsOfClub);

            AdapterOfKitColorsOfCountry = new dbTableAdapters.KitColorsOfCountryTableAdapter();
            TableOfKitColorsOfCountry = new db.KitColorsOfCountryDataTable();
            AdapterOfKitColorsOfCountry.Fill(TableOfKitColorsOfCountry);

            AdapterOfColor = new dbTableAdapters.ColorTableAdapter();
            TableOfColor = new db.ColorDataTable();
            AdapterOfColor.Fill(TableOfColor);

            AdapterOfManagerInfo = new dbTableAdapters.ManagerInfoTableAdapter();
            TableOfManagerInfo = new db.ManagerInfoDataTable();
            AdapterOfManagerInfo.Fill(TableOfManagerInfo);

            AdapterOfClubsOfChampionship = new dbTableAdapters.ClubsOfChampionshipTableAdapter();
            TableOfClubsOfChampionship = new db.ClubsOfChampionshipDataTable();
            AdapterOfClubsOfChampionship.Fill(TableOfClubsOfChampionship);

            AdapterOfSquadOfClub = new dbTableAdapters.SquadOfClubTableAdapter();
            TableOfSquadOfClub = new db.SquadOfClubDataTable();
            AdapterOfSquadOfClub.Fill(TableOfSquadOfClub);

        } 

        #region Игроки

        /// <summary>Возвращает игроков по заданному условию</summary>
		public static Player[] GetPlayers(string exp)
		{
			DataRow[] s = TableOfPlayerInfo.Select(exp);

			return getpl(s);
		}

		/// <summary>Возвращает всех игроков клуба</summary>
		public static Player[] GetPlayers(int ClubCode)
		{
            DataRow[] s = TableOfPlayerInfo.Select("Клуб = " + ClubCode);
            
			return getpl(s);
		}

		/// <summary>Возвращает игрока по его коду</summary>
		public static Player GetPlayer(int PlayerCode)
		{
            DataRow[] s = TableOfPlayerInfo.Select("Код = " + PlayerCode);
			return getpl(s)[0];
		}
		
		#endregion


		#region Клубы

		/// <summary>Возвращает клубы по заданному условию</summary>
		public static Club[] GetClubs(string exp)
		{
			DataRow[] s = TableOfClub.Select(exp);

			return getcl(s);
		}

		/// <summary>Возвращает клуб по его коду</summary>
		public static Club GetClub(int ClubCode)
		{
			DataRow[] s = TableOfClub.Select("Код = " + ClubCode);

			return getcl(s)[0];
		}


        public static Club[] GetClubsOfChamp(int ChampCode)
        {
            DataRow[] s = TableOfClubsOfChampionship.Select("Чемпионат = " + ChampCode);

            Club[] c = new Club[s.Length];

            for (int i = 0; i < s.Length; i++)
                c[i] = GetClub((int)s[i]["Клуб"]);

            return c;
        }
		#endregion


		#region Города

		/// <summary>Возвращает города по заданному условию</summary>
		public static City[] GetCities(string exp)
		{
			DataRow[] s = TableOfCity.Select(exp);

			return getci(s);
		}

		/// <summary>Возвращает город по его коду</summary>
		public static City GetCity(int CityCode)
		{
			DataRow[] s = TableOfCity.Select("Код = " + CityCode);
			return getci(s)[0];

		}

		#endregion


		#region Кубки

		/// <summary>Возвращает кубки по заданному условию</summary>
		public static Championship[] GetChampionships(string exp)
		{
			DataRow[] s = TableOfChampionship.Select(exp);

			return getch(s);
		}

		/// <summary>Возвращает кубок по его коду</summary>
		public static Championship GetChampionship(int ChampionshipCode)
		{
			DataRow[] s = TableOfChampionship.Select("Код = " + ChampionshipCode);
			return getch(s)[0];
		}
        
        /// <summary>Возвращает кубки, в которых участвует данный клуб</summary>
        public static Championship[] GetChampionships(int ClubCode)
        {
            DataRow[] s = TableOfClubsOfChampionship.Select("Клуб = " + ClubCode);

            Championship[] c = new Championship[s.Length];

            for (int i = 0; i < s.Length; i++)
                c[i] = GetChampionship((int)s[i]["Чемпионат"]);

            return c;
        }

        public static Championship[] GetChampionships(int ClubCode, TypeOfChamp Type)
        {
            DataRow[] s = TableOfClubsOfChampionship.Select("Клуб = " + ClubCode);
            List<Championship> c = new List<Championship>();

            for (int i = 0; i < s.Length; i++)
            {
                Championship ch = GetChampionship((int)s[i]["Чемпионат"]);
                if (ch.Type == (int)Type)
                    c.Add(ch);
            }
            return c.ToArray();
        }
		#endregion


		#region Стадионы

		/// <summary>Возвращает стадион по условию</summary>
		public static Stadium[] GetStadiums(string exp)
		{
			DataRow[] s = TableOfStadium.Select(exp);

			return getst(s);
		}

		/// <summary>Возвращает стадион по коду</summary>
		public static Stadium GetStadium(int StadiumCode)
		{
			DataRow[] s = TableOfStadium.Select("Код = " + StadiumCode);

			return getst(s)[0];
		}

		#endregion


		#region Матчи

		/// <summary>Возвращает матчи клубов по заданному условию</summary>
		public static MatchOfClub[] GetMatchesOfClubs(string exp)
		{
			DataRow[] s = TableOfMatchOfClubs.Select(exp);

            return (MatchOfClub[])getma(s);
		}

		/// <summary>Возвращает матчи клубов по диапазону дат</summary>
		public static MatchOfClub[] GetMatchesOfClubs(DateTime Start, DateTime End)
		{
			DataRow[] s;

			if (Start == End)
			{
				s = TableOfMatchOfClubs.Select("Дата = " + Start.ToShortDateString());
			}
			else
			{
				s = TableOfMatchOfClubs.Select("Дата > " + Start.ToShortDateString() + "AND Дата <" + End.ToShortDateString());
			}

            return (MatchOfClub[])getma(s);
		}

		/// <summary>Возвращает матч клуба по его коду</summary>
		public static MatchOfClub GetMatchOfClub(int MatchCode)
		{
			DataRow[] s = TableOfMatchOfClubs.Select("Код = " + MatchCode);

            return (MatchOfClub)getma(s)[0];
		}

        /// <summary>Возвращает матчи стран по заданному условию</summary>
        public static MatchOfCountry[] GetMatchesOfCountry(string exp)
        {
            DataRow[] s = TableOfMatchOfCountries.Select(exp);

            return (MatchOfCountry[])getma(s);
        }

        /// <summary>Возвращает матчи стран по диапазону дат</summary>
        public static MatchOfCountry[] GetMatchesOfCountries(DateTime Start, DateTime End)
        {
            DataRow[] s;

            if (Start == End)
            {
                s = TableOfMatchOfCountries.Select("Дата = " + Start.ToShortDateString());
            }
            else
            {
                s = TableOfMatchOfCountries.Select("Дата > " + Start.ToShortDateString() + "AND Дата <" + End.ToShortDateString());
            }

            return (MatchOfCountry[])getma(s);
        }

        /// <summary>Возвращает матч страны по его коду</summary>
        public static MatchOfCountry GetMatchOfCountry(int MatchCode)
        {
            DataRow[] s = TableOfMatchOfCountries.Select("Код = " + MatchCode);

            return (MatchOfCountry)getma(s)[0];
        }

		#endregion


		#region PlayedMatchesOfPlayer

		public static PlayedMatches[] GetPlayedMatches(string exp)
		{
			DataRow[] s = TableOfPlayedMatchesOfPlayer.Select(exp);

			return getplmaofpl(s);
		}

		public static PlayedMatches[] GetPlayedMatches(int PlayerCode)
		{
			DataRow[] s = TableOfPlayedMatchesOfPlayer.Select("Игрок = " + PlayerCode);

			return getplmaofpl(s);
		}

		public static PlayedMatches GetPlayedMatch(int PlayedMatchCode)
		{
			DataRow[] s = TableOfPlayedMatchesOfPlayer.Select("Код = " + PlayedMatchCode);

			return getplmaofpl(s)[0];
		}

		#endregion


		#region Травмы

		public static Injury[] GetInjuries(string exp)
		{
			DataRow[] s = TableOfInjury.Select(exp);

			return getinj(s);
		}

		public static Injury[] GetInjuries(int PlayerCode)
		{
			DataRow[] s = TableOfInjury.Select("Игрок = " + PlayerCode);

			return getinj(s);
		}

		public static Injury GetInjury(int InjuryCode)
		{
			DataRow[] s = TableOfInjury.Select("Код = " + InjuryCode);

			return getinj(s)[0];
		}

		#endregion


		#region Страны
		/// <summary>Возвращает название страны по её коду</summary>
		public static Country GetCountry(int CountryCode)
		{
			DataRow[] s = TableOfCountry.Select("Код = " + CountryCode);
			return getco(s)[0];
		}

		public static Country[] GetCountries(string exp)
		{
			DataRow[] s = TableOfCountry.Select(exp);
			return getco(s);
		}
		#endregion


		#region Трансферы

		public static Transfer[] GetTransfers(string exp)
		{
			DataRow[] s = TableOfTransfer.Select(exp);

			return gettransf(s);
		}

		public static Transfer[] GetTransfers(int PlayerCode)
		{
			DataRow[] s = TableOfTransfer.Select("Игрок = " + PlayerCode);

			return gettransf(s);
		}
		
		#endregion


        #region Цвета
        internal static KitColor[] GetColorsOfClub(int ClubCode)
        {
            DataRow[] s = TableOfKitColorsOfClub.Select("Клуб = " + ClubCode);

            KitColor[] c = new KitColor[s.Length];

            for (int i = 0; i < s.Length; i++)
                c[i] = new KitColor(s[i]);

            return c;
        }

        internal static KitColor[] GetColorsOfCountry(int CountryCode)
        {
            DataRow[] s = TableOfKitColorsOfCountry.Select("Страна = " + CountryCode);

            KitColor[] c = new KitColor[s.Length];

            for (int i = 0; i < s.Length; i++)
                c[i] = new KitColor(s[i]);

            return c;
        }

        internal static Color GetColor(int? code)
        {
            if (code.HasValue)
            {
                DataRow s = TableOfColor.FindByКод(code.Value);

                return Color.FromArgb(System.Convert.ToInt32(s["Цвет"].ToString().Remove(0, 1), 16));
            }
            else return Color.Black;
        }
        #endregion

		/// <summary>Возвращает название амплуа по его коду</summary>
		public static string GetAmplua(int AmpluaCode)
		{
			DataRow r = TableOfAmplua.Select("Код = " + AmpluaCode)[0];
			return (string)r[1];
		}
	    
		/// <summary>Возвращает название типа кубка по его коду</summary>
		public static string GetTypeOfChamp(int TypeOfChampCode)
		{
			DataRow r = TableOfTypeOfChamp.Select("Код = " + TypeOfChampCode)[0];
			return (string)r[1];
		}
		
        //public static void MovePlayer(Player Player, Club Club)
        //{
        //    db.PersonRow r = TableOfPerson.FindByКод(Player.Code);
        //    r.Клуб = Club.Code;
        //    TableOfPerson.ImportRow(r);
        //    TableOfPerson.AcceptChanges();
        //    AdapterOfPerson.Update(r);
        //}

		#region (internal) Информация, аттрибуты

		internal static PlayerInfo GetPlayerInfo(int PlayerCode)
		{
			DataRow p = TableOfPerson.FindByКод(PlayerCode);
			DataRow pi = TableOfPlayerInfo.FindByКод(PlayerCode);

            object Country = p["Страна2"];
            object ArendClub = pi["Аренда"];
            object Name = p["Имя"];

            if (Name is DBNull) { Name = ""; }
            if (Country is DBNull) { Country = 0; }
            if (ArendClub is DBNull) { ArendClub = 0; }

			return new PlayerInfo(
				PlayerCode,
                (string)Name,
				(string)p["Фамилия"],
				(int)p["Репутация"],
				(int)pi["Клуб"],
				p["ДР"].ToString(),
				(int)p["Страна1"],
                (int)Country,
				(int)pi["Номер"],
				(int)pi["Амплуа"],
				(int)pi["Рост"],
				(int)pi["Вес"],
                (int)ArendClub,
				(int)pi["Приоритет способностей"]
				);
		}

		internal static PhysicalAttr GetPhysAttr(int PlayerCode)
		{
			DataRow s = TableOfAttributes.FindByКод(PlayerCode);

			return new PhysicalAttr(
                PlayerCode,
				(int)s["Выносливость"],
				(int)s["Ловкость"],
				(int)s["Прыгучесть"],
				(int)s["Сила"],
				(int)s["Скорость"],
				(int)s["Ускорение"],
				(int)s["Баланс"],
				(int)s["Реакция"],
				(int)s["Усталость"]
				);
		}

		internal static MentalAttr GetMentalAttr(int PlayerCode)
		{
			DataRow s = TableOfAttributes.FindByКод(PlayerCode);

            return new MentalAttr(
                PlayerCode,
				(int)s["Выбор позиции"],
				(int)s["Командная игра"],
				(int)s["Концентрация"],
				(int)s["Принятие решений"],
				(int)s["Созидание"],
				(int)s["Самообладание"],
				(int)s["Тактика"],
				(int)s["Опека"],
				(int)s["Мораль"]
				);
		}

		internal static TechAttr GetTechAttr(int PlayerCode)
		{
			DataRow s = TableOfAttributes.FindByКод(PlayerCode);

            return new TechAttr(
                PlayerCode,
				(int)s["Дриблинг"],
				(int)s["Завершение атаки"],
				(int)s["Игра головой"],
				(int)s["Штрафные удары"],
				(int)s["Навесы"],
				(int)s["Отбор"],
				(int)s["Пас"],
				(int)s["Техника"],
				(int)s["Дальние удары"]
				);
		}

		internal static GKAttr GetGKAttr(int PlayerCode)

		{
			DataRow s = TableOfAttributes.FindByКод(PlayerCode);

            return new GKAttr(
                PlayerCode,
				(int)s["Ввод мяча"],
				(int)s["Игра в воздухе"],
				(int)s["Игра руками"],
				(int)s["Один на один"],
				(int)s["Организация обороны"],
				(int)s["Рефлекс"]
				);
		}

		internal static RefereeInfo GetRefereeInfo(int RefereeCode)
		{
			DataRow p = TableOfPerson.FindByКод(RefereeCode);
			DataRow ri = TableOfRefereeInfo.FindByКод(RefereeCode);

            object Name = p["Имя"];

            if (Name is DBNull) { Name = ""; }

			return new RefereeInfo(
				RefereeCode,
                (string)Name,
				(string)p["Фамилия"],
				(int)p["Репутация"],
				p["ДР"].ToString(),
				(int)p["Граж1"],
				(int)p["Граж2"],
				(int)ri["Город"]
				);
		}

		//TODO
        internal static ManagerInfo GetManagerInfo(Manager Manager)
		{
            DataRow p = TableOfPerson.FindByКод(Manager.Code);
            DataRow mi = TableOfManagerInfo.FindByКод(Manager.Code);

            object Name = p["Имя"];
			object Country1 = p["Страна1"];
			object Country2 = p["Страна2"];
			object tacticIndex = mi["Тактика"];
			object defIndex = mi["Расстановка защиты"];
			object midIndex = mi["Расстановка полузащиты"];
			object frwIndex = mi["Расстановка нападения"];
			
            if (Name is DBNull) { Name = ""; }
			if (Country1 is DBNull)
			{
				System.Random RND = new System.Random();
				Country1 = RND.Next(1, 221); 
			}
			if (Country2 is DBNull) { Country2 = 0; }
			if (tacticIndex is DBNull) { tacticIndex = 4; }
			if (defIndex is DBNull) { defIndex = 1; }
			if (midIndex is DBNull) { midIndex = 1; }
			if (frwIndex is DBNull) { frwIndex = 1; }
			RingBuffer Tactic = new RingBuffer(7, (int)tacticIndex); //TODO
            RingBuffer Def = new RingBuffer(0, (int)defIndex);
            RingBuffer Mid = new RingBuffer(0, (int)midIndex);
            RingBuffer Frw = new RingBuffer(0, (int)frwIndex);

			return new ManagerInfo(
				Manager.Code,
                (string)Name,
				(string)p["Фамилия"],
				(int)p["Репутация"],
				p["ДР"].ToString(),
				(int)Country1,
				(int)Country2,
				(RingBuffer)Tactic,
				(RingBuffer)Def,
				(RingBuffer)Mid,
				(RingBuffer)Frw
				);
		}
		
		#endregion

		public static int GetPriceValue(int PlayerCode)
		{
			DataRow s = TableOfPriceValue.FindByКод(PlayerCode);

			return (int)s["Стоимость"];
		}

        public static TypeOfInjury GetTypeOfInjury(int TypeOfInjuryCode)
		{
			DataRow s = TableOfTypeOfInjury.FindByКод(TypeOfInjuryCode);

			return new TypeOfInjury(
				(int)s["Код"],
				(string)s["Тип"],
				(int)s["Минимум дней"],
				(int)s["Максимум дней"],
				(bool)s["Только при игре"]
				);
		}

        public static void DeleteInjury(Injury i)
        {
            AdapterOfInjury.Delete(i.Code, i.PlayerCode, i.Type.Code, i.RecoverDate);
            AdapterOfInjury.Update(TableOfInjury);
            AdapterOfInjury.Fill(TableOfInjury);
        }

        internal static Manager GetManager(int Code)
        {
            DataRow s = TableOfManagerInfo.FindByКод(Code);
            return new Manager((int)s["Код"]);
        }

		#region Работа с изыманием информации о тактике
        internal static int[] GetCountOfPlayersInAllLines(RingBuffer Tactic)
		{
			DataRow t = TableOfTotalTactic.FindByКод(Tactic.Value);
			return new int[] { (int)t["Защита"], (int)t["Полузащита"], (int)t["Нападение"] };
		}

        internal static int GetCountOfPlayersInLine(RingBuffer Tactic, int Line)
		{
			return GetCountOfPlayersInAllLines(Tactic)[Line - 1];
		}

        internal static int Opredelitel(Manager manager, int Line, bool ReturnAlignment)
        {
			int[] CountOfPlayersInAllLine = GetCountOfPlayersInAllLines(manager.Info.Tactic);

			int CountOfPlayersInLine = 0;
            int FormationOfLine = 0;

			switch (Line)
            {
                case (int)(FootballAmplua.Defence):
					CountOfPlayersInLine = CountOfPlayersInAllLine[Convert.ToInt32(FootballAmplua.Defence) - 1];
					if (ReturnAlignment) 
					{
						FormationOfLine = manager.Info.AlignmentOfDefence.Value; 
					}
                    break;
				case (int)(FootballAmplua.Middlefield):
					CountOfPlayersInLine = CountOfPlayersInAllLine[Convert.ToInt32(FootballAmplua.Middlefield) - 1];
                    if (ReturnAlignment) 
					{
						FormationOfLine = manager.Info.AlignmentOfMiddlefield.Value; 
					}
                    break;
				case (int)(FootballAmplua.Attack):
					CountOfPlayersInLine = CountOfPlayersInAllLine[Convert.ToInt32(FootballAmplua.Attack) - 1];
					if (ReturnAlignment) 
					{
						FormationOfLine = manager.Info.AlignmentOfForward.Value; 
					}
                    break;
                default:
                    break;
            }

			return (Convert.ToInt32(Line) * 100 + CountOfPlayersInLine * 10 + FormationOfLine);
        }

		public static System.Windows.Point[] GetLocationsOfPlayersOfClub(Manager manager)
        {
            int[] Opr = new int[3];

			List<db.TacticPointRow> DataTableOfTacticPoint = new List<db.TacticPointRow>();

			for (int i = 0; i < 3; i++)
			{
				Opr[i] = Opredelitel(manager, i + 1, true);
			}

			for (int i = 0; i < Opr.Length; i++)
            {
				db.TacticPointRow[] TTR = (db.TacticPointRow[]) TableOfTacticPoint.Select("Определитель = " + Opr[i]);
				TTR = TTR.OrderBy(x => x["Номер"]).ToArray();
				DataTableOfTacticPoint.AddRange(TTR);
            }

			System.Windows.Point[] Result = new System.Windows.Point[DataTableOfTacticPoint.Count + 1];

			Result[0] = new System.Windows.Point(950, 340);
			for (int i = 0; i < DataTableOfTacticPoint.Count; i++)
			{
				db.TacticPointLocationRow TPLR = TableOfTacticPointLocation.FindByКод(DataTableOfTacticPoint[i].Точка);
				Result[i + 1] = new System.Windows.Point(TPLR.X, TPLR.Y);
			}

			return Result;
        }

        public static int GetCountOfTactics(Manager manager, int Line)
        {
            int opr = Opredelitel(manager, Line, false);
            return TableOfTacticOfLine.Select("Определитель = " + opr).Length;
        }

        public static string GetNameOfAlignment(int Opr)
        {
            DataRow s = TableOfTacticOfLine.Select("Определитель = " + ((Opr / 10) * 10) + " AND Формация = " + (Opr % ((Opr / 10) * 10)))[0];
            return (string)s["Текст"];
        }
		#endregion

        public static HistoryOfClub[] GetHistoryOfClub(int ClubCode)
        {
            DataRow[] s = TableOfHistoryOfClub.Select("Клуб = "+ ClubCode);
            HistoryOfClub[] result = new HistoryOfClub[s.Length];

            for (int i = 0; i < s.Length; i++)
            {

                object ect = s[i]["Дополнительно"];

                if (ect is DBNull) ect = "";

                result[i] = new HistoryOfClub(
                    ClubCode,
                    (int)s[i]["Клуб"],
                    (string)s[i]["Сезон"],
                    (int)s[i]["Дивизион"],
                    (int)s[i]["Выигрыши"],
                    (int)s[i]["Ничьи"],
                    (int)s[i]["Поражения"],
                    (int)s[i]["Голы"],
                    (int)s[i]["Пропущенные мячи"],
                    (int)s[i]["Позиция"],
                    (string)ect);
            }

            return result;
        }

        public static HistoryOfChampionship[] GetHistoryOfChampionship(int ChampCode)
        {
            db.HistoryOfChampionshipRow[] s = (db.HistoryOfChampionshipRow[])TableOfHistoryOfChampionship.Select("Клуб = " + ChampCode);
            HistoryOfChampionship[] result = new HistoryOfChampionship[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                object etc = s[i]["Дополнительно"];

                if (etc is DBNull) etc = "";

                result[i] = new HistoryOfChampionship(
                    ChampCode,
                    (string)s[i]["Сезон"],
                    (int)s[i]["Чемпионат"],
                    (int)s[i]["Первое место"],
                    (int)s[i]["Второе место"],
                    (int)s[i]["Третье место"],
                    (string)s[i]["Бомбардиры"],
                    (string)etc);
            }

            return result;
        }

		#region Добавление

        public static void AddInjury(int PlayerCode, int Type, DateTime RecoverDate)
        {
            AdapterOfInjury.Insert(PlayerCode, Type, RecoverDate);
            AdapterOfInjury.Update(TableOfInjury);
            AdapterOfInjury.Fill(TableOfInjury);
        }
        
        public static void AddMatch(int Code,DateTime Date,int Championship,int Round,int Home,int Guest,int GoalHome,int GoalGuest,int PenaltyHome,int PenaltyGuest)
        {
            AdapterOfMatchOfClubs.Insert(Code, Date, Championship, Round, Home, Guest, GoalHome, GoalGuest, PenaltyHome, PenaltyGuest);
            AdapterOfMatchOfClubs.Update(TableOfMatchOfClubs);
            AdapterOfMatchOfClubs.Fill(TableOfMatchOfClubs);
        }
        
        #endregion

        #region Изменение

        public static void UpdateAttributes(db.AttributesRow p)
        {
            AdapterOfAttributes.Update(p);
            AdapterOfAttributes.Fill(TableOfAttributes);
            
        }

      
        #endregion

        #region Методы

        private static Player[] getpl(DataRow[] s)
		{
			Player[] Players = new Player[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Players[i] = new Player((int)s[i]["Код"]);
			}

			return Players;
		}

		private static Club[] getcl(DataRow[] s)
		{
			Club[] Clubs = new Club[s.Length];

            
			for (int i = 0; i < s.Length; i++)
			{
                object city = s[i]["Город"];
                object budget = s[i]["Бюджет"];
                object rep = s[i]["Репутация"];
                object st = s[i]["Стадион"];
                object manager = s[i]["Менеджер"];

                if (city is DBNull) city = 0;
                if (budget is DBNull) budget = 0;
                if (rep is DBNull) rep = 0;
                if (st is DBNull) st = 0;
                if (manager is DBNull) manager = 0;

				Clubs[i] = new Club(
					(int)s[i]["Код"],
					(string)s[i]["Полное название"],
					(string)s[i]["Короткое название"],
                    (int)city,
                    (int)budget,
                    (int)rep,
                    (int)st,
                    (int)manager
				);
			}
			return Clubs;
		}

		private static Championship[] getch(DataRow[] s)
		{
			Championship[] Championships = new Championship[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Championships[i] = new Championship(
					(int)s[i]["Код"],
					(string)s[i]["Название"],
					(int)s[i]["Страна"],
					(int)s[i]["Тип"],
					(int)s[i]["Клубов"],
					(int)s[i]["Репутация"],
					(bool)s[i]["Активирован"]
				);
			}
			return Championships;
		}

		private static City[] getci(DataRow[] s)
		{
			City[] Cities = new City[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Cities[i] = new City(
					(int)s[i]["Код"],
					(string)s[i]["Город"],
					(int)s[i]["Репутация"],
					(int)s[i]["Страна"]
				);
			}
			return Cities;
		}

		private static Stadium[] getst(DataRow[] s) 
		{
			Stadium[] Stadiums = new Stadium[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Stadiums[i] = new Stadium(
					(int)s[i]["Код"],
					(string)s[i]["Название"],
					(int)s[i]["Город"],
					(int)s[i]["Вместимость"]
				);
			}
			return Stadiums;
		}

		private static Match[] getma(DataRow[] s)
		{
			Match[] Matches = new Match[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Matches[i] = new Match(
					(int)s[i]["Код"],
					(DateTime)s[i]["Дата"],
					(int)s[i]["Турнир"],
					(int)s[i]["Тур"],
					(int)s[i]["Дом"],
					(int)s[i]["Гости"],
					(int)s[i]["Гол Д"],
					(int)s[i]["Гол Г"],
					(int)s[i]["Пенальти Д"],
					(int)s[i]["Пенальти Г"]
				);
			}
			return Matches;
		}

		private static PlayedMatches[] getplmaofpl(DataRow[] s)
		{
			PlayedMatches[] PlayedMatches = new PlayedMatches[ s.Length ];

			for (int i = 0; i < s.Length; i++)
			{
				PlayedMatches[ i ] = new PlayedMatches(
					(int)s[ i ][ "Код" ],
					(int)s[ i ][ "Игрок" ],
					(int)s[ i ][ "Матч" ],
					(bool)s[ i ][ "Сторона" ],
					(int)s[ i ][ "Голы" ],
					(int)s[ i ][ "Гол передачи" ],
					(int)s[ i ][ "ЖК" ],
					(int)s[ i ][ "КК" ],
					(int)s[ i ][ "Минуты" ],
					(double)s[ i ][ "Оценка" ]
					);
			}

			return PlayedMatches;
		}

		private static Country[] getco(DataRow[] s)
		{
			Country[] Countries = new Country[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
                object manager = s[i]["Менеджер"];
                if (manager is DBNull) manager = 0;
				Countries[i] = new Country(
					(int)s[i]["Код"],
					(string)s[i]["Страна"],
					(int)s[i]["Репутация"],
					(bool)s[i]["Активирован"],
                    (int)manager
					);
			}

			return Countries;
		}

		private static Injury[] getinj(DataRow[] s)
		{
			Injury[] Injuries = new Injury[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Injuries[i] = new Injury(
					(int)s[i]["Код"],
					(int)s[i]["Игрок"],
					(int)s[i]["Тип"],
					(DateTime)s[i]["Восстановление"]
					);
			}

			return Injuries;
		}

		private static Transfer[] gettransf(DataRow[] s)
		{
			Transfer[] Transfers = new Transfer[s.Length];

			for (int i = 0; i < s.Length; i++)
			{
				Transfers[i] = new Transfer(
					(int)s[i]["Код"],
					(int)s[i]["Игрок"],
					(int)s[i]["Откуда"],
					(int)s[i]["Куда"],
					s[i]["Дата"].ToString(),
					(int)s[i]["Сумма"]
					);
			}

			return Transfers;
		}

	#endregion
	}

  
  
}

