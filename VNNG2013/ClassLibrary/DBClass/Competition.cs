using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;
using ClassLibrary.Interfaces;
using System.Windows.Media;

namespace ClassLibrary.DBClass
{
	[Entity("Соревнование", true)]
	[ItemsCollection("Информация", "Клубы", "История")]
	public class Competition : DBObject, IWindowData
	{
		private string name;
		[DBAttribute]
		[EditingProperty("Название соревнования")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		public enum CompetitionType
		{
			[FieldTranslation("Высший национальный чемпионат")]
			TheHiestNationalChampionship,
			[FieldTranslation("Национальный чемпионат")]
			NationalChampionship,
			[FieldTranslation("Национальный чемпионат с подлигами")]
			NationalChampionshipWithUnderLeagues,
			[FieldTranslation("Национальный кубок")]
			NationalCup,
			[FieldTranslation("Национальный супер-кубок")]
			NationalSuperCup,
			[FieldTranslation("Подлига")]
			UnderLeague/*,
			[FieldTranslation("Группы + Плей-офф (международный клубный турнир)")]
			GroupsAndPlayOff_Clubs,
			[FieldTranslation("Группы + Плей-офф (международный турнир сборных)")]
			GroupsAndPlayOff_Nations,
			[FieldTranslation("Квалификация клубов")]
			ClubCvalification*/
		}
		private CompetitionType type;
		[DBAttribute]
		[EditingProperty("Тип")]
		public CompetitionType Type
		{
			get { return this.type; }
			set
			{
				this.type = value;
				if (this.DataStorage != null)
				{
					if (this.Type == CompetitionType.NationalCup || this.Type == CompetitionType.NationalSuperCup || this.Type == CompetitionType.TheHiestNationalChampionship)
					{
						Competition[] competitions = this.DataStorage.GetDBObjects(typeof(Competition).GetEntity())
							.Where(x => x != this && (x as Competition).FootballOrganizationID == this.FootballOrganizationID && (x as Competition).Type == this.Type)
							.Cast<Competition>().ToArray();
						if (competitions != null)
							foreach (Competition competition in competitions)
								//Знуляем всех остальных
								competition.FootballOrganization = null;
					}
				}
				this.ToNotifyChanges("Type");
			}
		}

		private bool isActivated;
		[DBAttribute]
		[IsVisibleIf("Type", CompetitionType.TheHiestNationalChampionship, CompetitionType.NationalChampionship, CompetitionType.NationalChampionshipWithUnderLeagues, CompetitionType.NationalCup, CompetitionType.NationalSuperCup)]
		[EditingProperty("Активирован?")]
		public bool IsActivated
		{
			get { return this.isActivated; }
			set { this.isActivated = value; this.ToNotifyChanges("IsActivated"); }
		}

		[DBAttribute]
		internal id FootballOrganizationID;
		private FootballOrganization footballOrganization;
		[IsVisibleIf("Type", CompetitionType.TheHiestNationalChampionship, CompetitionType.NationalCup, CompetitionType.NationalSuperCup)]
		[EditingProperty("Принадлежность к футбольной организации")]
		public FootballOrganization FootballOrganization
		{
			get { return this.GetDBObjectPropertyValue<FootballOrganization>("FootballOrganization"); }
			set
			{
				this.SetDBObjectPropertyValue("FootballOrganization", value);
				if (this.DataStorage != null)
				{
					if (this.Type == CompetitionType.NationalCup || this.Type == CompetitionType.NationalSuperCup || this.Type == CompetitionType.TheHiestNationalChampionship)
					{
						Competition[] competitions = this.DataStorage.GetDBObjects(typeof(Competition).GetEntity())
							.Where(x => x != this && (x as Competition).FootballOrganizationID == this.FootballOrganizationID && (x as Competition).Type == this.Type)
							.Cast<Competition>().ToArray();
						if (competitions != null)
							foreach (Competition competition in competitions)
								//Знуляем всех остальных
								competition.FootballOrganization = null;
					}
				}
			}
		}

		private int clubsCount;
		[DBAttribute]
		[IsVisibleIf("Type", CompetitionType.TheHiestNationalChampionship, CompetitionType.NationalChampionship, CompetitionType.UnderLeague)]
		[EditingProperty("Количество клубов")]
		public int ClubsCount
		{
			get { return this.clubsCount; }
			set { this.clubsCount = value; this.ToNotifyChanges("ClubsCount"); }
		}
		public static readonly DependencyProperty ClubsCountProperty = DependencyProperty.Register("ClubsCount", typeof(int), typeof(Competition));

		[DBAttribute]
		internal id HierCompetitionID;
		private Competition hierCompetition;
		[IsVisibleIf("Type", CompetitionType.NationalChampionship, CompetitionType.NationalChampionshipWithUnderLeagues)]
		[EditingProperty("Более высокое соревнование")]
		[Condition(MemberType.Method, "CanBeHierCompetition")]
		public Competition HierCompetition
		{
			get { return this.GetDBObjectPropertyValue<Competition>("HierCompetition"); }
			set { this.SetDBObjectPropertyValue("HierCompetition", value); }
		}

		private int clubsCountForHierCompetition;
		[DBAttribute]
		[IsVisibleIf("Type", CompetitionType.NationalChampionship, CompetitionType.UnderLeague)]
		[Limit(IntegerLimit.Positive)]
		[EditingProperty("Количество команд, повышающихся в классе напрямую")]
		public int ClubsCountForHierCompetition
		{
			get { return this.clubsCountForHierCompetition; }
			set { this.clubsCountForHierCompetition = value; this.ToNotifyChanges("ClubsCountForHierCompetition"); }
		}

		private int clubsCountToStyksToHierCompetition;
		[DBAttribute]
		[IsVisibleIf("Type", CompetitionType.NationalChampionship, CompetitionType.UnderLeague)]
		[Limit(IntegerLimit.PositiveOrZero)]
		[EditingProperty("Количество команд, участвующих в стыках")]
		public int ClubsCountToStyksToHierCompetition
		{
			get { return this.clubsCountToStyksToHierCompetition; }
			set { this.clubsCountToStyksToHierCompetition = value; this.ToNotifyChanges("ClubsCountToStyksToHierCompetition"); }
		}

		[EditingProperty("Клубы на следующий сезон", "Клубы")]
		[IsVisibleIf("Type", CompetitionType.TheHiestNationalChampionship, CompetitionType.NationalChampionship, CompetitionType.UnderLeague)]
		public Club[] NextSeasonClubs
		{
			get { return this.GetBindedArrayPropertyValue<Club>("NextSeasonClubs", "League"); }
			set { this.SetBindedArrayPropertyValue("NextSeasonClubs", value, "League"); }
		}

		public Club[] Clubs
		{
			get { return this.GetBindedArrayPropertyValue<Club>(typeof(CompetitionClub)); }
		}

		[EditingProperty("История", "История")]
		[IsVisibleIf("Type", CompetitionType.TheHiestNationalChampionship, CompetitionType.NationalChampionship, CompetitionType.NationalCup, CompetitionType.NationalSuperCup, CompetitionType.UnderLeague)]
		public HistoryOfCompetition[] History
		{
			get { return this.GetBindedArrayPropertyValue<HistoryOfCompetition>("History", "Competition"); }
			set { this.SetBindedArrayPropertyValue("History", value, "Competition"); }
		}








		public Competition() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();

			this.Name = "Новое соревнование";
			this.Type = CompetitionType.TheHiestNationalChampionship;
			this.IsActivated = true;
			this.ClubsCountForHierCompetition = 1;
		}

		public override string DisplayedText
		{
			get 
			{
				return string.Format("{0} ({1})", this.Name, this.GetTypeName());
			}
		}

		public string HeaderText
		{
			get { return this.Name; }
		}

		public DBObject UpLevel
		{
			get
			{
				if (this.FootballOrganization != null)
					if (this.FootballOrganization.Nations != null)
						if (this.FootballOrganization.Nations.Length > 0)
							return this.FootballOrganization.Nations.First();
				return null;
			}
		}

		public string UpLevelText
		{
			get
			{
				if (this.UpLevel != null)
					return (this.UpLevel as Nation).Name;
				return null;
			}
		}

		public Brush ForegroundBrush
		{
			get
			{
				Nation nation = this.FootballOrganization.Nations.FirstOrDefault();
				if (nation != null)
					return nation.ForegroundBrush;
				else
					return Brushes.Red;
			}
		}

		public Brush BackgroundBrush
		{
			get
			{
				Nation nation = this.FootballOrganization.Nations.FirstOrDefault();
				if (nation != null)
					return nation.BackgroundBrush;
				else
					return Brushes.White;
			}
		}

		public Brush BorderBrush
		{
			get
			{
				Nation nation = this.FootballOrganization.Nations.FirstOrDefault();
				if (nation != null)
					return nation.BorderBrush;
				else
					return Brushes.Black;
			}
		}

		public DBObject Next
		{
			get
			{
				//TODO
				return this;
			}
		}

		public DBObject Previous
		{
			get
			{
				//TODO
				return this;
			}
		}







		public bool IsTheHiestNationalChampionship()
		{
			return this.Type == CompetitionType.TheHiestNationalChampionship;
		}

		public bool IsNationalChampionship()
		{
			return this.Type == CompetitionType.NationalChampionship;
		}

		public bool IsNationalChampionshipWithUnderLeagues()
		{
			return this.Type == CompetitionType.NationalChampionshipWithUnderLeagues;
		}

		public bool IsNationalCup()
		{
			return this.Type == CompetitionType.NationalCup;
		}

		public bool IsNationalSuperCup()
		{
			return this.Type == CompetitionType.NationalSuperCup;
		}

		public bool IsUnderLeague()
		{
			return this.Type == CompetitionType.UnderLeague;
		}

		public string GetTypeName()
		{
			return (typeof(CompetitionType).GetField(this.Type.ToString()).GetAttribute<FieldTranslationAttribute>() as FieldTranslationAttribute).FieldTranslateName;
		}

		public bool CanBeHierCompetition()
		{
			return (this.Type == CompetitionType.TheHiestNationalChampionship || 
				this.Type == CompetitionType.NationalChampionship ||
				this.Type == CompetitionType.UnderLeague);
		}

		public bool IsLeague()
		{
			return this.Type == CompetitionType.TheHiestNationalChampionship ||
				this.Type == CompetitionType.NationalChampionship ||
				this.Type == CompetitionType.UnderLeague;
		}

	}
}
