using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Attributes;
using System.Windows;
using ClassLibrary.Interfaces;
using System.IO;
using System.Windows.Media.Imaging;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Windows.Media;

namespace ClassLibrary.DBClass
{
	[Entity("Персона", true)]
	[ItemsCollection("Информация", "Клуб", "Сборная", "Позиции", "Характеристики", "Семья")]
	public class Person : DBObject, IWindowData
	{
		/*private string fullName;
		[DBAttribute(false)]
		[EditingProperty("Полное имя (на Латинице)", "Информация")]
		public string FullName
		{
			get { return this.fullName; }
			set 
			{ 
				this.fullName = value; 
				this.ToNotifyChanges("FullName");
				this.ToNotifyChanges("FullNameat");
				this.ToNotifyChanges("FullName_Cyr");
			}
		}

		private string fullName_C;
		[DBAttribute(false)]
		[EditingProperty("Полное имя (на Кириллице)", "Информация")]
		public string FullName_C
		{
			get { return this.fullName_C; }
			set 
			{ 
				this.fullName_C = value; 
				this.ToNotifyChanges("FullName_C");
				this.ToNotifyChanges("FullNameat");
				this.ToNotifyChanges("FullName_Cyr"); 
			}
		}

		[EditingProperty("Полное имя (на Кириллице)", "Информация", true)]
		public string FullName_Cyr
		{
			get { return string.IsNullOrWhiteSpace(this.FullName_C) ? this.FullName.Translate() : this.FullName_C; }
		}*/

		private string fullName;
		[DBAttribute(false)]
		[EditingProperty("Полное имя (на Латинице)", "Информация")]
		public string FullName
		{
			get { return this.fullName; }
			set { this.fullName = value; this.ToNotifyChanges("FullName"); }
		}

		private string name;
		[DBAttribute(false)]
		[EditingProperty("Имя (на Латинице)", "Информация")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		private string surname;
		[DBAttribute(false)]
		[EditingProperty("Фамилия (на Латинице)", "Информация")]
		public string Surname
		{
			get { return this.surname; }
			set { this.surname = value; this.ToNotifyChanges("Surname"); }
		}

		private string nickname;
		[DBAttribute(false)]
		[EditingProperty("Прозвище (на Латинице)", "Информация")]
		public string Nickname
		{
			get { return this.nickname; }
			set { this.nickname = value; this.ToNotifyChanges("Nickname"); }
		}

        private string strizhke;
        [DBAttribute(false)]
        [Limit(10)]
        [EditingProperty("Стрижка", "Информация")]
        public string Strizhke
        {
            get { return this.strizhke; }
            set { this.strizhke = value; this.ToNotifyChanges("Strizhke"); }
        }

        private string motherName;
        [DBAttribute(false)]
        [Limit(20)]
        [EditingProperty("Имя мамы", "Семья")]
        public string MotherName
        {
            get { return this.motherName; }
            set { this.motherName = value; this.ToNotifyChanges("MotherName"); }
        }

		public enum PersonType
		{
			[FieldTranslation("Игрок")]
			Player,
			[FieldTranslation("Главный тренер")]
			Manager,
			[FieldTranslation("Судья")]
			Referee,
			[FieldTranslation("Руководитель")]
			Director,
			[FieldTranslation("Завершил карьеру")]
			Retired
		}
		private PersonType type;
		[DBAttribute]
		[EditingProperty("Тип", "Информация")]
		public PersonType Type
		{
			get { return this.type; }
			set 
			{
				if (this.DataStorage != null && (value == PersonType.Manager || value == PersonType.Director))
				{
					//Менеджер у клуба должен быть только один!
					Person[] persons = this.DataStorage.GetDBObjects(new DBEntity(typeof(Person))).Where(x => x != this && (x as Person).ClubID == this.ClubID && (x as Person).Type == value).Cast<Person>().ToArray();
					if (persons != null)
						foreach (Person person in persons)
							//Знуляем всех остальных
							person.Club = null;
				}
				this.type = value; 
				this.ToNotifyChanges("Type");
			}
		}

		private DateTime birthdayDate;
		[DBAttribute(false)]
		[EditingProperty("Дата рождения", "Информация")]
		[IsVisibleIf("Type", PersonType.Player, PersonType.Manager)]
		public DateTime BirthdayDate
		{
			get { return this.birthdayDate; }
			set { this.birthdayDate = value; this.ToNotifyChanges("BirthdayDate"); }
		}

		[DBAttribute]
		internal id CityID;
		private City city;
		[EditingProperty("Город", "Информация")]
		[IsVisibleIf("Type", PersonType.Referee)]
		public City City
		{
			get { return this.GetDBObjectPropertyValue<City>("City"); }
			set { this.SetDBObjectPropertyValue("City", value); }
		}

		[DBAttribute]
		internal id NationID;
		private Nation nation;
		[EditingProperty("Нация", "Информация")]
		public Nation Nation
		{
			get { return this.GetDBObjectPropertyValue<Nation>("Nation"); }
			set { this.SetDBObjectPropertyValue("Nation", value); }
		}

		private int reputation;
		[DBAttribute]
		[EditingProperty("Репутация", "Информация")]
		[IsVisibleIf("Type", PersonType.Player, PersonType.Manager)]
		[Limit(IntegerLimit.RatingLarge)]
		public int Reputation
		{
			get { return this.reputation; }
			set { this.reputation = value; this.ToNotifyChanges("Reputation"); }
		}

		[DBAttribute]
		internal id ClubID;
		private Club club;
		[EditingProperty("Клуб", "Клуб")]
		[IsVisibleIf("Type", PersonType.Player, PersonType.Manager, PersonType.Director)]
		public Club Club
		{
			get { return this.GetDBObjectPropertyValue<Club>("Club"); }
			set 
			{
				if (this.DataStorage != null && value != null && (this.IsManager || this.IsDirector))
				{
					//Менеджер у клуба должен быть только один!
					Person[] persons = this.DataStorage.GetDBObjects(new DBEntity(typeof(Person))).Where(x => x != this && (x as Person).ClubID == value.ID && (x as Person).Type == this.Type).Cast<Person>().ToArray();
					if (persons != null)
						foreach (Person person in persons)
							//Знуляем всех остальных
							person.Club = null;
				}
				this.SetDBObjectPropertyValue("Club", value);
			}
		}

		[DBAttribute]
		internal id NationTeamID;
		private Nation nationTeam;
		[EditingProperty("Главный тренер сборной", "Сборная")]
		[IsVisibleIf("Type", PersonType.Manager)]
		public Nation NationTeam
		{
			get { return this.GetDBObjectPropertyValue<Nation>("NationTeam"); }
			set
			{
				if (this.DataStorage != null && value != null && this.IsManager)
				{
					//Менеджер у сборной должен быть только один!
					Person[] persons = this.DataStorage.GetDBObjects(new DBEntity(typeof(Person))).Where(x => x != this && (x as Person).NationTeamID == value.ID && (x as Person).Type == this.Type).Cast<Person>().ToArray();
					if (persons != null)
						foreach (Person person in persons)
							//Знуляем всех остальных
							person.NationTeam = null;
				}
				this.SetDBObjectPropertyValue("NationTeam", value);
			}
		}

		private int nationTeamGamesCount;
		[DBAttribute]
		[EditingProperty("Количество игр за сборную", "Сборная")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int NationTeamGamesCount
		{
			get { return this.nationTeamGamesCount; }
			set { this.nationTeamGamesCount = value; this.ToNotifyChanges("NationTeamGamesCount"); }
		}

		private int nationTeamGoalsCount;
		[DBAttribute]
		[EditingProperty("Количество голов за сборную", "Сборная")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int NationTeamGoalsCount
		{
			get { return this.nationTeamGoalsCount; }
			set { this.nationTeamGoalsCount = value; this.ToNotifyChanges("NationTeamGoalsCount"); }
		}

		private int nationTeamU21GamesCount;
		[DBAttribute]
		[EditingProperty("Количество игр за молодёжную сборную", "Сборная")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int NationTeamU21GamesCount
		{
			get { return this.nationTeamU21GamesCount; }
			set { this.nationTeamU21GamesCount = value; this.ToNotifyChanges("NationTeamU21GamesCount"); }
		}

		private int nationTeamU21GoalsCount;
		[DBAttribute]
		[EditingProperty("Количество голов за молодёжную сборную", "Сборная")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int NationTeamU21GoalsCount
		{
			get { return this.nationTeamU21GoalsCount; }
			set { this.nationTeamU21GoalsCount = value; this.ToNotifyChanges("NationTeamU21GoalsCount"); }
		}

		[DBAttribute]
		internal id InLoanClubID;
		private Club inLoanClub;
		[IsVisibleIf("Type", PersonType.Player)]
		[EditingProperty("Клуб для аренды", "Клуб")]
		public Club InLoanClub
		{
			get { return this.GetDBObjectPropertyValue<Club>("InLoanClub"); }
			set { this.SetDBObjectPropertyValue("InLoanClub", value); }
		}

		private int height;
		[DBAttribute]
		[EditingProperty("Рост (см)", "Информация")]
		[IsVisibleIf("Type", PersonType.Player)]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int Height
		{
			get { return this.height; }
			set { this.height = value; this.ToNotifyChanges("Height"); }
		}

		private int weight;
		[DBAttribute]
		[EditingProperty("Вес (кг)", "Информация")]
		[IsVisibleIf("Type", PersonType.Player)]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int Weight
		{
			get { return this.weight; }
			set { this.weight = value; this.ToNotifyChanges("Weight"); }
		}

		private DateTime contractUntil;
		[DBAttribute]
		[EditingProperty("Контракт истекает", "Информация")]
		[IsVisibleIf("Type", PersonType.Player, PersonType.Manager)]
		public DateTime ContractUntil
		{
			get { return this.contractUntil; }
			set { this.contractUntil = value; this.ToNotifyChanges("ContractUntil"); }
		}

		private int playingNumber;
		[DBAttribute]
		[EditingProperty("Игровой номер", "Клуб")]
		[IsVisibleIf("Type", PersonType.Player)]
		[Limit(0, 99)]
		public int PlayingNumber
		{
			get { return this.playingNumber; }
			set { this.playingNumber = value; this.ToNotifyChanges("PlayingNumber"); }
		}

		/* Приоритет способностей */

		public enum StandartTacticType
		{
			[FieldTranslation("5-4-1")]
			Tactic_5_4_1,
			[FieldTranslation("5-3-2")]
			Tactic_5_3_2,
			[FieldTranslation("4-5-1")]
			Tactic_4_5_1,
			[FieldTranslation("4-4-2")]
			Tactic_4_4_2,
			[FieldTranslation("4-3-3")]
			Tactic_4_3_3,
			[FieldTranslation("3-5-2")]
			Tactic_3_5_2,
			[FieldTranslation("3-4-3")]
			Tactic_3_4_3
		}
		[DBAttribute]
		private StandartTacticType likedStandartTactic;
		[DBAttribute]
		[IsVisibleIf("Type", PersonType.Manager)]
		[EditingProperty("Любимая стандартная тактика", "Информация")]
		public StandartTacticType LikedStandartTactic
		{
			get { return this.likedStandartTactic; }
			set
			{
				this.likedStandartTactic = value;
				this.ToNotifyChanges("LikedStandartTactic");
			}
		}

		[Sorting("ID")]
		public Tactic[] Tactics
		{
			get { return this.GetBindedArrayPropertyValue<Tactic>("Tactics", "Manager"); }
			set { this.SetBindedArrayPropertyValue("Tactics", value); }
		}

		public enum WorkingFootType
		{
			[FieldTranslation("Правая")]
			Right,
			[FieldTranslation("Левая")]
			Left,
			[FieldTranslation("Обе")]
			Both
		}
		private WorkingFootType workingFoot;
		[DBAttribute]
		[IsVisibleIf("Type", PersonType.Player)]
		[EditingProperty("Рабочая нога", "Характеристики")]
		public WorkingFootType WorkingFoot
		{
			get { return this.workingFoot; }
			set
			{
				this.workingFoot = value;
				this.ToNotifyChanges("WorkingFoot");
			}
		}
		public string WorkingFootString
		{
			get
			{
				if (this.WorkingFoot == WorkingFootType.Right)
					return "Правая";
				else if (this.WorkingFoot == WorkingFootType.Left)
					return "Левая";
				else
					return "Обе";
			}
		}


		#region Способность инрать на позиции
		private enum AmpluaPositionType
		{
			[FieldTranslation("ВР")]
			Goalkeeper,
			[FieldTranslation("З")]
			Defender,
			[FieldTranslation("ОП")]
			DefensiveMiddlefielder,
			[FieldTranslation("П")]
			Middlefielder,
			[FieldTranslation("АП")]
			AttackingMiddlefielder,
			[FieldTranslation("В")]
			Winger,
			[FieldTranslation("Н")]
			Striker
		}
		private enum SidePositionType
		{
			[FieldTranslation("")]
			None,
			[FieldTranslation("Л")]
			Left,
			[FieldTranslation("Ц")]
			Centeral,
			[FieldTranslation("П")]
			Right
		}
		[AttributeUsage(AttributeTargets.Property)]
		private class PositionAbilityAttribute : Attribute 
		{
			private AmpluaPositionType amplua;
			public AmpluaPositionType Amplua
			{
				get
				{
					return this.amplua;
				}
			}

			private SidePositionType side;
			public SidePositionType Side
			{
				get
				{
					return this.side;
				}
			}

			public PositionAbilityAttribute(AmpluaPositionType amplua, SidePositionType side = SidePositionType.None)
			{
				this.amplua = amplua;
				this.side = side;
			}
		}

		private bool? goalkeeperAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Goalkeeper)]
		[EditingProperty("Вратарь", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? GoalkeeperAbility
		{
			get { return this.goalkeeperAbility; }
			set { this.goalkeeperAbility = value; this.ToNotifyChanges("GoalkeeperAbility"); }
		}

		private bool? leftDefAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Defender, SidePositionType.Left)]
		[EditingProperty("Левый защитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? LeftDefAbility
		{
			get { return this.leftDefAbility; }
			set { this.leftDefAbility = value; this.ToNotifyChanges("LeftDefAbility"); }
		}

		private bool? centerDefAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Defender, SidePositionType.Centeral)]
		[EditingProperty("Центральный защитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? CenterDefAbility
		{
			get { return this.centerDefAbility; }
			set { this.centerDefAbility = value; this.ToNotifyChanges("CenterDefAbility"); }
		}

		private bool? rightDefAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Defender, SidePositionType.Right)]
		[EditingProperty("Правый защитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? RightDefAbility
		{
			get { return this.rightDefAbility; }
			set { this.rightDefAbility = value; this.ToNotifyChanges("RightDefAbility"); }
		}

		private bool? defensiveMidAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.DefensiveMiddlefielder)]
		[EditingProperty("Опорный полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? DefensiveMidAbility
		{
			get { return this.defensiveMidAbility; }
			set { this.defensiveMidAbility = value; this.ToNotifyChanges("DefensiveMidAbility"); }
		}

		private bool? leftMidAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Middlefielder, SidePositionType.Left)]
		[EditingProperty("Левый полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? LeftMidAbility
		{
			get { return this.leftMidAbility; }
			set { this.leftMidAbility = value; this.ToNotifyChanges("LeftMidAbility"); }
		}

		private bool? centralMidAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Middlefielder, SidePositionType.Centeral)]
		[EditingProperty("Центральный полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? CentralMidAbility
		{
			get { return this.centralMidAbility; }
			set { this.centralMidAbility = value; this.ToNotifyChanges("CentralMidAbility"); }
		}

		private bool? rightMidAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Middlefielder, SidePositionType.Right)]
		[EditingProperty("Правый полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? RightMidAbility
		{
			get { return this.rightMidAbility; }
			set { this.rightMidAbility = value; this.ToNotifyChanges("RightMidAbility"); }
		}

		private bool? leftWingAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.AttackingMiddlefielder, SidePositionType.Left)]
		[EditingProperty("Левый атакующий полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? LeftWingAbility
		{
			get { return this.leftWingAbility; }
			set { this.leftWingAbility = value; this.ToNotifyChanges("LeftWingAbility"); }
		}

		private bool? attackingMidAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.AttackingMiddlefielder, SidePositionType.Centeral)]
		[EditingProperty("Центральный атакующий полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? AttackingMidAbility
		{
			get { return this.attackingMidAbility; }
			set { this.attackingMidAbility = value; this.ToNotifyChanges("AttackingMidAbility"); }
		}

		private bool? rightWingAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.AttackingMiddlefielder, SidePositionType.Right)]
		[EditingProperty("Правый атакующий полузащитник", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? RightWingAbility
		{
			get { return this.rightWingAbility; }
			set { this.rightWingAbility = value; this.ToNotifyChanges("RightWingAbility"); }
		}

		private bool? strikerAbility;
		[DBAttribute]
		[PositionAbility(AmpluaPositionType.Striker)]
		[EditingProperty("Нападающий", "Позиции")]
		[IsVisibleIf("Type", PersonType.Player)]
		public bool? StrikerAbility
		{
			get { return this.strikerAbility; }
			set { this.strikerAbility = value; this.ToNotifyChanges("StrikerAbility"); }
		}
		#endregion



		public BitmapImage Face
		{
			get
			{
				string source = string.Format(SpecialPaths.DefaultFace);
				DirectoryInfo dir = new DirectoryInfo(SpecialPaths.Faces);
				foreach (FileInfo file in dir.GetFiles())
				{
					if (Path.GetFileNameWithoutExtension(file.Name) == this.ID.ToString())
					{
						source = string.Format("{0}\\{1}", SpecialPaths.Faces, file.Name);
						break;
					}
				}
				return new BitmapImage(new Uri(source));
			}
		}

		public int Age
		{
			get
			{
				return this.DataStorage.CurrentDate.Difference(this.BirthdayDate).Year;
			}
		}

		public string AgeString
		{
			get
			{
				return this.Age.GetAgeString();
			}
		}







		public Person() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();

			this.Name = "Новый";
			this.Surname = "Человек";
			this.BirthdayDate = new DateTime(1985, 1, 1);
			this.Type = PersonType.Player;
			this.ContractUntil = new DateTime(2014, 7, 1);
		}

		public override string DisplayedText
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.Nickname))
					return this.Nickname;
				else if (!string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.Surname))
					return string.Format("{0}, {1}", this.Surname, this.Name);
				else if (!string.IsNullOrWhiteSpace(this.Surname))
					return this.Surname;
				else
					return this.Name;
			}
		}

		public string HeaderText
		{
			get { return this.DisplayedText; }
		}

		public DBObject UpLevel
		{
			get
			{
				if (this.InLoanClub != null)
					return this.InLoanClub;
				if (this.Club != null)
					return this.Club;
				return null;
			}
		}

		public string UpLevelText
		{
			get 
			{
				if (this.UpLevel != null)
					return (this.UpLevel as Club).FullName;
				return null;
			}
		}

		public override string GetXAMLDataTemplate()
		{
			return
			"<TextBlock>" +
				"<Run Text=\"{Binding Path=Name}\" /> \n " +
				"<Run Text=\"{Binding Path=Surname}\" /> \n " +
				"<Run Text=\" (\" />" +
				"<Run Text=\"{Binding Path=Profession}\" />" +
				"<Run Text=\")\" />" +
			"</TextBlock>";
		}

		public Brush ForegroundBrush
		{
			get
			{
				if (this.CurrentClub != null)
					return this.CurrentClub.ForegroundBrush;
				else
					return this.Nation.ForegroundBrush;
			}
		}

		public Brush BackgroundBrush
		{
			get
			{
				if (this.CurrentClub != null)
					return this.CurrentClub.BackgroundBrush;
				else
					return this.Nation.BackgroundBrush;
			}
		}

		public Brush BorderBrush
		{
			get
			{
				if (this.CurrentClub != null)
					return this.CurrentClub.BorderBrush;
				else
					return this.Nation.BorderBrush;
			}
		}

		public DBObject Next
		{
			get
			{
				IEnumerable<Person> players = this.Club.Players.OrderBy(x => x.ToString());
				int indexOfThis = players.OrderBy(x => x.ToString()).IndexOf(this);
				int nextIndex = (indexOfThis + 1) % players.Count();
				return players.ElementAt(nextIndex);
			}
		}

		public DBObject Previous
		{
			get
			{
				IEnumerable<Person> players = this.Club.Players.OrderBy(x => x.ToString());
				int indexOfThis = players.OrderBy(x => x.ToString()).IndexOf(this);
				int previousIndex = (indexOfThis + players.Count() - 1) % players.Count();
				return players.ElementAt(previousIndex);
			}
		}
	




		public bool IsPlayer
		{
			get { return this.Type == PersonType.Player; }
		}

		public bool IsManager
		{
			get { return this.Type == PersonType.Manager; }
		}

		public bool IsReferee
		{
			get { return this.Type == PersonType.Referee; }
		}

		public bool IsDirector
		{
			get { return this.Type == PersonType.Director; }
		}

		public bool IsRetired
		{
			get { return this.Type == PersonType.Retired; }
		}
		
		public string Profession
		{
			get
			{
				return typeof(PersonType).GetField(this.Type.ToString()).GetAttribute<FieldTranslationAttribute>().FieldTranslateName;
			}
			set { }
		}

		/// <summary> Получает список позиций в виде строки, на которых может играть данный игрок. </summary>
		public string PositionsListString
		{
			get
			{
				StringBuilder Result = new StringBuilder();
				List<PropertyInfo> allAbilityProperties = typeof(Person).GetProperties<PositionAbilityAttribute>();
				foreach(FieldInfo amplua in typeof(AmpluaPositionType).GetFields(BindingFlags.Public | BindingFlags.Static))
				{
					string ampluaString = amplua.GetAttribute<FieldTranslationAttribute>().FieldTranslateName;
					Result.Append(string.Format("{0}(", ampluaString));
					PropertyInfo[] ampluaProperties = allAbilityProperties
						.Where(x => x.GetAttribute<PositionAbilityAttribute>().Amplua == (AmpluaPositionType)amplua.GetValue(null))
						.ToArray();
					bool valueIsSet = false;
					foreach (PropertyInfo positionAbilityProperty in ampluaProperties)
					{
						if ((positionAbilityProperty.GetValue(this, null) as bool?) != null)
						{
							valueIsSet = true;
							SidePositionType sideString = positionAbilityProperty.GetAttribute<PositionAbilityAttribute>().Side;
							FieldInfo fieldSide = typeof(SidePositionType).GetField(sideString.ToString());
							Result.Append(fieldSide.GetAttribute<FieldTranslationAttribute>().FieldTranslateName);
						}
					}

					if (Result.ToString().Last() == '(')
					{
						Result.Remove(Result.Length - 1, 1);
						if (!valueIsSet)
							Result.Remove(Result.Length - ampluaString.Length, ampluaString.Length);
						else
							Result.Append(" ");
					}
					else
						Result.Append(") ");

				}
				return Result.ToString();
			}
		}

		public Club CurrentClub
		{
			get
			{
				if (this.InLoanClub != null)
					return this.InLoanClub;
				else
					return this.Club;
			}
		}

		/// <summary> Показывает, имеет ли Персона "полное имя" </summary>
		public Visibility HasFullName
		{
			get { return string.IsNullOrWhiteSpace(this.FullName) ? Visibility.Collapsed : Visibility.Visible; }
		}

		/// <summary> Показывает, отсутствует ли у Персоны "полное имя" </summary>
		public Visibility HasNotFullName
		{
			get { return !string.IsNullOrWhiteSpace(this.FullName) ? Visibility.Collapsed : Visibility.Visible; }
		}
	}
}
