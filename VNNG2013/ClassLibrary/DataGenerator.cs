using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.DBClass;
using ClassLibrary.Attributes;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary.Resources.Tactics;
using ClassLibrary.Resources.Tactics.Classes;
using System.Windows;

namespace ClassLibrary
{
	public class DataGenerator
	{
		private DataStorage dataStorage;

		private Random random;

		public DataGenerator(DataStorage dataStorage)
		{
			this.dataStorage = dataStorage;
			this.dataStorage.CreateGameData();
			this.random = new Random();
		}

		/// <summary> Генерирует данные Хранилища данных пригодными для игры </summary>
		public void Generate()
		{
			this.FillSpaces();
			IEnumerable<MethodInfo> doItMathods = typeof(DataGenerator)
														.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
														.Where(x => x.HasAttribute<DoItAttribute>());
			foreach (MethodInfo method in doItMathods)
			{
				method.Invoke(this, null);
			}
		}

		/// <summary> Заполняет "пробелы" данных во всех объектах Хранилища данных </summary>
		private void FillSpaces()
		{
			foreach(DBEntity entity in dataStorage.GetEntities())
			{
				MethodInfo method = typeof(DataGenerator).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).SingleOrDefault(x => x.Name == entity.Name);

				if (method != null)
					foreach (DBObject dbObject in dataStorage.GetDBObjects(entity).ToArray())
						method.Invoke(this, BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dbObject }, null);
			}
		}

		#region Методы для FillSpaces()
		private int GetRandomValue(int value, IntegerLimit limit)
		{
			int Result = 0;
			switch(limit)
			{
				case IntegerLimit.Rating:
					if (value == 0) Result = this.random.Next(1, 10);
					else if (value == -1) Result = this.random.Next(5, 7);
					else if (value == -2) Result = this.random.Next(8, 10);
					break;
				case IntegerLimit.RatingLarge:
					if (value == 0) Result = this.random.Next(1, 100);
					else if (value == -1) Result = this.random.Next(50, 79);
					else if (value == -2) Result = this.random.Next(80, 100);
					break;
				default:
					throw new ArgumentException("Аргумент 'limit' должен иметь значение 'Rating' или 'RatingLarge'");
			}
			return Result;
		}

		private void Nation(Nation nation)
		{
			if (string.IsNullOrWhiteSpace(nation.Name))
				nation.Name = "Некая нация";
			if (string.IsNullOrWhiteSpace(nation.Abbreviation))
				nation.Abbreviation = nation.Name.Substring(0, 3);
			if (nation.Reputation <= 0)
				nation.Reputation = this.GetRandomValue(nation.Reputation, IntegerLimit.RatingLarge);
		}

		private void City(City city)
		{
			if (string.IsNullOrWhiteSpace(city.Name))
				city.Name = "Некий город";
			if (city.Population == 0)
				city.Population = this.random.Next(100, 50000);
			if (city.Reputation.NotPositive())
				city.Reputation = this.GetRandomValue(city.Reputation, IntegerLimit.RatingLarge);
		}

		private void Stadium(Stadium stadium)
		{
			if (string.IsNullOrWhiteSpace(stadium.Name))
				stadium.Name = "Некий стадион";
			if (stadium.Capacity == 0)
				stadium.Capacity = this.random.Next(100, 2000);
		}

		private void Person(Person person)
		{
			if (string.IsNullOrWhiteSpace(person.Name + person.Surname + person.Nickname))
			{
				this.dataStorage.DeleteDBObject(person);
				return;
			}
			if (person.BirthdayDate.Year > 1998)
				person.BirthdayDate = new DateTime(1998, person.BirthdayDate.Month, person.BirthdayDate.Day);
			if (person.BirthdayDate.Year < 1932)
				person.BirthdayDate = new DateTime(1932, person.BirthdayDate.Month, person.BirthdayDate.Day);
			if (person.IsReferee && person.CityID.IsNull)
				person.City = (City)this.dataStorage.GetRandomObject(typeof(City).GetEntity());
			if (person.Nation == null)
				person.Nation = (Nation)this.dataStorage.GetRandomObject(typeof(Nation).GetEntity());
			if (person.Reputation.NotPositive())
				person.Reputation = this.GetRandomValue(person.Reputation, IntegerLimit.RatingLarge);
			if (person.IsPlayer && person.ClubID.IsNull && person.InLoanClubID.HasValue)
			{
				person.ClubID = person.InLoanClubID;
				person.InLoanClubID = new id(null);
			}
			if (person.Height == 0)
				person.Height = this.random.Next(165, 190);
			if (person.Weight == 0)
				person.Weight = this.random.Next(58, 85);
			if (person.ClubID.HasValue)
				foreach (Person otherPerson in person.Club.Players.Where(x => x.PlayingNumber == person.PlayingNumber && x != person))
					otherPerson.PlayingNumber = 0;
		}

		private void Competition(Competition competition)
		{
			if (string.IsNullOrWhiteSpace(competition.Name))
				competition.Name = "Некое соревнование";
		}

		private void FootballOrganization(FootballOrganization fo)
		{
			if (string.IsNullOrWhiteSpace(fo.Name))
				fo.Name = "Некая организация";
		}

		private void HistoryOfCompetition(HistoryOfCompetition history)
		{
			if (history.CompetitionID.IsNull || history.FirstPlaceID.IsNull)
				this.dataStorage.DeleteDBObject(history);
		}

		private void Club(Club club)
		{
			if (string.IsNullOrWhiteSpace(club.Name))
				club.Name = "Некий клуб";
			if (string.IsNullOrWhiteSpace(club.ShortName))
				club.ShortName = club.Name.Substring(0, 6);
			if (club.Reputation.NotPositive())
				club.Reputation = this.GetRandomValue(club.Reputation, IntegerLimit.RatingLarge);
		}
		#endregion

		private void OtherWorks()
		{
			foreach (MethodInfo method in typeof(DataGenerator).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				method.Invoke(this, null);
			}
		}

		[AttributeUsage(AttributeTargets.Method)]
		private class DoItAttribute : Attribute { }

		[DoIt]
		/// <summary> Допускает клубы к участию в соревнованиях </summary>
		private void AddDBObjectClubsToParticipationOfCompetition()
		{
			foreach (Club club in this.dataStorage.GetDBObjects<Club>())
			{
				if (club.League != null)
				{
					CompetitionClub participation = new CompetitionClub() { Club = club, Competition = club.League };
					this.dataStorage.AddDBObject(participation);
				}
			}
		}

		[DoIt]
		/// <summary> Добавить для каждого главного тренера стандартную тактику по его предпочтению =) 
		/// ...и созранить их в Хранилище данных </summary>
		private void SetStandartTactics()
		{
			//Устанавливаем тактики главным тренерам
			foreach (Person manager in this.dataStorage.GetDBObjects<Person>().Where(x => x.IsManager))
			{
				Tactic tactic = this.CreateTactic((int)manager.LikedStandartTactic);
				tactic.Manager = manager;
			}

			//Устанавливаем тактики клубам
			foreach (Club club in this.dataStorage.GetDBObjects<Club>())
			{
				if (club.Manager != null)
					club.CurrentTactic = club.Manager.Tactics.First();
				else
					club.CurrentTactic = this.CreateTactic();
			}
		}

		/// <summary> Создаёт тактику из стандартной тактики и сохраняет её в Хранилище Данных </summary>
		private Tactic CreateTactic(int tacticKey = -1)
		{
			Random rnd = new Random();
			if (tacticKey == -1) tacticKey = rnd.Next(0, StandartTactics.GeneralTactics.Count);
			GeneralTactic generalTactic = StandartTactics.GeneralTactics[tacticKey];
			int defKey = rnd.Next(0, StandartTactics.LineFormations.Where(x => x.LineKey == 1 && x.PlayersCount == generalTactic.DefendersCount).Count());
			int midKey = rnd.Next(0, StandartTactics.LineFormations.Where(x => x.LineKey == 2 && x.PlayersCount == generalTactic.MidfieldersCount).Count());
			int strKey = rnd.Next(0, StandartTactics.LineFormations.Where(x => x.LineKey == 3 && x.PlayersCount == generalTactic.StrikersCount).Count());
			List<Point> locations = StandartTactics.GeneralTactics[tacticKey].GetLocations(defKey, midKey, strKey);
			Tactic Result = this.dataStorage.CreateDBObject<Tactic>();
			Result.Name = "Своя тактика";
			for (int I = 0; I < 11; I++)
			{
				PositionInfo position = this.dataStorage.CreateDBObject<PositionInfo>();
				position.X = locations[I].X;
				position.Y = locations[I].Y;
				position.Number = I;
				position.Tactic = Result;
			}
			return Result;
		}


	
	}
}
