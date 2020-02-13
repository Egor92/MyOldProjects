using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;
using ClassLibrary.Interfaces;
using System.Windows.Media;
using System.Reflection;

namespace ClassLibrary.DBClass
{
	[Entity("Клуб", true)]
	[ItemsCollection("Информация", "Игроки", "Форма")]
	public class Club : DBObject, IWindowData
	{
		private string fullName;
		[DBAttribute]
		[EditingProperty("Полное название", "Информация")]
		public string FullName
		{
			get { return this.fullName; }
			set { this.fullName = value; this.ToNotifyChanges("FullName"); }
		}

		private string name;
		[DBAttribute]
		[EditingProperty("Обычное название", "Информация")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		private string shortName;
		[DBAttribute(false)]
		[EditingProperty("Короткое название (до 6 символов)", "Информация")]
		[Limit(6)]
		public string ShortName
		{
			get { return this.shortName; }
			set { this.shortName = value; this.ToNotifyChanges("ShortName"); }
		}

		private string nickName;
		[DBAttribute(false)]
		[EditingProperty("Прозвище", "Информация")]
		public string NickName
		{
			get { return this.nickName; }
			set { this.nickName = value; this.ToNotifyChanges("NickName"); }
		}

		[DBAttribute]
		internal id CityID;
		private City city;
		[EditingProperty("Город", "Информация")]
		public City City
		{
			get { return this.GetDBObjectPropertyValue<City>("City"); }
			set { this.SetDBObjectPropertyValue("City", value); }
		}

		[EditingProperty("Мэнеджер", "Информация")]
		[Condition(MemberType.Property, "IsManager")]
		public Person Manager
		{
			get { return this.GetBindedPropertyValue<Person>("Manager"); }
			set { this.SetBindedPropertyValue("Manager", value); }
		}

		[DBAttribute]
		internal id StadiumID;
		private Stadium stadium;
		[EditingProperty("Стадион", "Информация")]
		public Stadium Stadium
		{
			get { return this.GetDBObjectPropertyValue<Stadium>("Stadium"); }
			set { this.SetDBObjectPropertyValue("Stadium", value); }
		}

		private int budget;
		[DBAttribute]
		[EditingProperty("Бюджет", "Информация")]
		public int Budget
		{
			get { return this.budget; }
			set { this.budget = value; this.ToNotifyChanges("Budget"); }
		}

		private int reputation;
		[DBAttribute]
		[EditingProperty("Репутация", "Информация")]
		[Limit(IntegerLimit.RatingLarge)]
		public int Reputation
		{
			get { return this.reputation; }
			set { this.reputation = value; this.ToNotifyChanges("Reputation"); }
		}

		[EditingProperty("Игроки", "Игроки")]
		[Condition(MemberType.Property, "IsPlayer")]
		public Person[] Players
		{
			get { return this.GetBindedArrayPropertyValue<Person>("Players"); }
			set { this.SetBindedArrayPropertyValue("Players", value); }
		}

		[EditingProperty("Форма", "Форма")]
		[Sorting("Number")]
		public ClubsKit[] Kits
		{
			get { return this.GetBindedArrayPropertyValue<ClubsKit>("Kits"); }
			set { this.SetBindedArrayPropertyValue("Kits", value); }
		}

		public ClubsKit HomeKit
		{
			get
			{
				ClubsKit homeKit = this.Kits.Where(x => x.Number != 0).FirstOrDefault();
				if (homeKit != null)
					return homeKit;
				else
					return ClubsKit.Default;
			}
		}

		public ClubsKit GKKit
		{
			get
			{
				ClubsKit gkKit = this.Kits.Where(x => x.Number == 0).FirstOrDefault();
				if (gkKit != null)
					return gkKit;
				else
					return ClubsKit.GKDefault;
			}
		}

		[DBAttribute]
		internal id LeagueID;
		private Competition league;
		[EditingProperty("Чемпионат", "Информация")]
		[Condition(MemberType.Method, "IsLeague")]
		public Competition League
		{
			get { return this.GetDBObjectPropertyValue<Competition>("League"); }
			set { this.SetDBObjectPropertyValue("League", value); }
		}



		#region Позиции игроков
		[DBAttribute]
		internal id Player00ID;
		private Person player00;
		public Person Player00
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player00"); }
			set { this.SetDBObjectPropertyValue("Player00", value); }
		}

		[DBAttribute]
		internal id Player01ID;
		private Person player01;
		public Person Player01
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player01"); }
			set { this.SetDBObjectPropertyValue("Player01", value); }
		}

		[DBAttribute]
		internal id Player02ID;
		private Person player02;
		public Person Player02
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player02"); }
			set { this.SetDBObjectPropertyValue("Player02", value); }
		}

		[DBAttribute]
		internal id Player03ID;
		private Person player03;
		public Person Player03
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player03"); }
			set { this.SetDBObjectPropertyValue("Player03", value); }
		}

		[DBAttribute]
		internal id Player04ID;
		private Person player04;
		public Person Player04
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player04"); }
			set { this.SetDBObjectPropertyValue("Player04", value); }
		}

		[DBAttribute]
		internal id Player05ID;
		private Person player05;
		public Person Player05
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player05"); }
			set { this.SetDBObjectPropertyValue("Player05", value); }
		}

		[DBAttribute]
		internal id Player06ID;
		private Person player06;
		public Person Player06
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player06"); }
			set { this.SetDBObjectPropertyValue("Player06", value); }
		}

		[DBAttribute]
		internal id Player07ID;
		private Person player07;
		public Person Player07
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player07"); }
			set { this.SetDBObjectPropertyValue("Player07", value); }
		}

		[DBAttribute]
		internal id Player08ID;
		private Person player08;
		public Person Player08
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player08"); }
			set { this.SetDBObjectPropertyValue("Player08", value); }
		}

		[DBAttribute]
		internal id Player09ID;
		private Person player09;
		public Person Player09
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player09"); }
			set { this.SetDBObjectPropertyValue("Player09", value); }
		}

		[DBAttribute]
		internal id Player10ID;
		private Person player10;
		public Person Player10
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player10"); }
			set { this.SetDBObjectPropertyValue("Player10", value); }
		}

		[DBAttribute]
		internal id Player11ID;
		private Person player11;
		public Person Player11
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player11"); }
			set { this.SetDBObjectPropertyValue("Player11", value); }
		}

		[DBAttribute]
		internal id Player12ID;
		private Person player12;
		public Person Player12
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player12"); }
			set { this.SetDBObjectPropertyValue("Player12", value); }
		}

		[DBAttribute]
		internal id Player13ID;
		private Person player13;
		public Person Player13
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player13"); }
			set { this.SetDBObjectPropertyValue("Player13", value); }
		}

		[DBAttribute]
		internal id Player14ID;
		private Person player14;
		public Person Player14
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player14"); }
			set { this.SetDBObjectPropertyValue("Player14", value); }
		}

		[DBAttribute]
		internal id Player15ID;
		private Person player15;
		public Person Player15
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player15"); }
			set { this.SetDBObjectPropertyValue("Player15", value); }
		}

		[DBAttribute]
		internal id Player16ID;
		private Person player16;
		public Person Player16
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player16"); }
			set { this.SetDBObjectPropertyValue("Player16", value); }
		}

		[DBAttribute]
		internal id Player17ID;
		private Person player17;
		public Person Player17
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player17"); }
			set { this.SetDBObjectPropertyValue("Player17", value); }
		}

		[DBAttribute]
		internal id Player18ID;
		private Person player18;
		public Person Player18
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player18"); }
			set { this.SetDBObjectPropertyValue("Player18", value); }
		}

		[DBAttribute]
		internal id Player19ID;
		private Person player19;
		public Person Player19
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player19"); }
			set { this.SetDBObjectPropertyValue("Player19", value); }
		}

		[DBAttribute]
		internal id Player20ID;
		private Person player20;
		public Person Player20
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player20"); }
			set { this.SetDBObjectPropertyValue("Player20", value); }
		}

		[DBAttribute]
		internal id Player21ID;
		private Person player21;
		public Person Player21
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player21"); }
			set { this.SetDBObjectPropertyValue("Player21", value); }
		}

		[DBAttribute]
		internal id Player22ID;
		private Person player22;
		public Person Player22
		{
			get { return this.GetDBObjectPropertyValue<Person>("Player22"); }
			set { this.SetDBObjectPropertyValue("Player22", value); }
		}
		#endregion

		public Dictionary<int, Person> Squad
		{
			get
			{
				Dictionary<int, Person> Result = new Dictionary<int, Person>();
				IEnumerable<PropertyInfo> allProperties = typeof(Club)
																.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				for (int I=0; I<23; I++)
				{
					PropertyInfo playerProperty = allProperties.Single(x => x.Name == string.Format("Player{0}", I.ToString("D2")));
					Result.Add(I, (Person)playerProperty.GetValue(this, null));
				}
				return Result;
			}
		}

		[DBAttribute]
		internal id CurrentTacticID;
		private Tactic currentTactic;
		public Tactic CurrentTactic
		{
			get { return this.GetDBObjectPropertyValue<Tactic>("CurrentTactic"); }
			set { this.SetDBObjectPropertyValue("CurrentTactic", value); }
		}



		public Club() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();

			this.FullName = "ФК Новый Клуб Город";
			this.Name = "Новый Клуб";
			this.ShortName = "Клуб";
		}

		public override string DisplayedText
		{
			get
			{
				return string.Format(
					"{0} ({1})",
					this.Name,
					this.City != null ? this.City.Name : "?");
			}
		}

		public string HeaderText
		{
			get { return this.FullName; }
		}

		public DBObject UpLevel
		{
			get { return this.League; }
		}

		public string UpLevelText
		{
			get { return this.UpLevel != null ? (this.UpLevel as Competition).Name : null; }
		}

		public Brush ForegroundBrush
		{
			get 
			{ 
				Kit[] kits = this.Kits.Where(x => x.Number != 0).OrderBy(x => x.Number).ToArray();
				if (kits.Length > 0)
					return new SolidColorBrush(kits.First().Foreground);
				else
					return Brushes.Red;
			}
		}

		public Brush BackgroundBrush
		{
			get
			{
				Color color;
				Kit[] kits = this.Kits.Where(x => x.Number != 0).OrderBy(x => x.Number).ToArray();
				if (kits.Length > 0)
					color = kits.First().Background;
				else
					color = Colors.White;
				double coef = 1.4;
				return new LinearGradientBrush(color.Multiply(coef), color.Multiply(1 / coef), 90.0);
			}
		}

		public Brush BorderBrush
		{
			get
			{
				Kit[] kits = this.Kits.Where(x => x.Number != 0).OrderBy(x => x.Number).ToArray();
				if (kits.Length > 0)
					return new SolidColorBrush(kits.First().Border);
				else
					return Brushes.Black;
			}
		}

		public DBObject Next
		{
			get
			{
				IEnumerable<Club> clubs = this.League.Clubs.OrderBy(x => x.ToString());
				int indexOfThis = clubs.IndexOf(this);
				int nextIndex = (indexOfThis + 1) % clubs.Count();
				return clubs.ElementAt(nextIndex);
			}
		}

		public DBObject Previous
		{
			get
			{
				IEnumerable<Club> clubs = this.League.Clubs.OrderBy(x => x.ToString());
				int indexOfThis = clubs.OrderBy(x => x.ToString()).IndexOf(this);
				int previousIndex = (indexOfThis + clubs.Count() - 1) % clubs.Count();
				return clubs.ElementAt(previousIndex);
			}
		}


	}
}
