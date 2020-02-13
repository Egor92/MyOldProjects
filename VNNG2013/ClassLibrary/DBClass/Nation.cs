using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;
using ClassLibrary.Interfaces;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace ClassLibrary.DBClass
{
	[Entity("Нация", true)]
	[ItemsCollection("Информация", "Форма")]
	public class Nation : DBObject, IWindowData
	{
		private string name;
		[DBAttribute]
		[EditingProperty("Название нации")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		/* Прилагательное */
		private string adjective;
		[DBAttribute(false)]
		[EditingProperty("Национальность (прилагательное)")]
		public string Adjective
		{
			get { return this.adjective; }
			set { this.adjective = value; this.ToNotifyChanges("Adjective"); }
		}

		private string abbreviation;
		[DBAttribute]
		[EditingProperty("Аббревиатура (до 3 символов)")]
		[Limit(3)]
		public string Abbreviation
		{
			get { return this.abbreviation; }
			set { this.abbreviation = value; this.ToNotifyChanges("Abbreviation"); }
		}

		private bool isNotExist;
		[DBAttribute]
		[EditingProperty("Ныне не существует")]
		public bool IsNotExist
		{
			get { return this.isNotExist; }
			set { this.isNotExist = value; this.ToNotifyChanges("IsNotExist"); }
		}

		private int reputation;
		[DBAttribute]
		[EditingProperty("Репутация")]
		[IsVisibleIf("IsNotExist", false)]
		[Limit(IntegerLimit.RatingLarge)]
		public int Reputation
		{
			get { return this.reputation; }
			set { this.reputation = value; this.ToNotifyChanges("Reputation"); }
		}

		private bool nationTeamHasActivated;
		[DBAttribute]
		[EditingProperty("Активтрована национальная сборная?")]
		[IsVisibleIf("IsNotExist", false)]
		public bool NationTeamHasActivated
		{
			get { return this.nationTeamHasActivated; }
			set { this.nationTeamHasActivated = value; this.ToNotifyChanges("NationTeamHasActivated"); }
		}

		[DBAttribute]
		internal id FootballOrganizationID;
		private FootballOrganization footballOrganization;
		[EditingProperty("Принадлежность к футбольной организации")]
		[IsVisibleIf("IsNotExist", false)]
		public FootballOrganization FootballOrganization
		{
			get { return this.GetDBObjectPropertyValue<FootballOrganization>("FootballOrganization"); }
			set { this.SetDBObjectPropertyValue("FootballOrganization", value); }
		}

		[EditingProperty("Главный тренер национальной сборной", "Информация")]
		[Condition(MemberType.Property, "IsManager")]
		[IsVisibleIf("IsNotExist", false)]
		public Person Manager
		{
			get { return this.GetBindedPropertyValue<Person>("Manager", "NationTeam"); }
			set { this.SetBindedPropertyValue("Manager", value, "NationTeam"); }
		}

		[EditingProperty("Форма", "Форма")]
		[Sorting("Number")]
		[IsVisibleIf("IsNotExist", false)]
		public NationsKit[] Kits
		{
			get { return this.GetBindedArrayPropertyValue<NationsKit>("Kits"); }
			set { this.SetBindedArrayPropertyValue("Kits", value); }
		}




		public BitmapImage Flag
		{
			get
			{
				string source = string.Format(SpecialPaths.DefaultCountry);
				DirectoryInfo dir = new DirectoryInfo(SpecialPaths.Countries);
				foreach (FileInfo file in dir.GetFiles())
				{
					if (Path.GetFileNameWithoutExtension(file.Name) == this.ID.ToString())
					{
						source = string.Format("{0}\\{1}", SpecialPaths.Countries, file.Name);
						break;
					}
				}
				return new BitmapImage(new Uri(source));
			}
		}








		public Nation() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();

			this.Name = "Новая нация";
			this.Adjective = "национальность";
		}

		public override string DisplayedText
		{
			get
			{
				return string.Format("{0}", this.Name);
			}
		}

		public string HeaderText
		{
			get { return this.Name; }
		}

		public DBObject UpLevel
		{
			get { return null; }
		}

		public string UpLevelText
		{
			get { return null; }
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
				IEnumerable<Nation> nations = this.DataStorage.GetDBObjects<Nation>().Where(x => !x.IsNotExist && x.NationTeamHasActivated).OrderBy(x => x.ToString());
				int indexOfThis = nations.OrderBy(x => x.ToString()).IndexOf(this);
				int nextIndex = (indexOfThis + 1) % nations.Count();
				return nations.ElementAt(nextIndex);
			}
		}

		public DBObject Previous
		{
			get
			{
				IEnumerable<Nation> nations = this.DataStorage.GetDBObjects<Nation>().Where(x => !x.IsNotExist && x.NationTeamHasActivated).OrderBy(x => x.ToString());
				int indexOfThis = nations.OrderBy(x => x.ToString()).IndexOf(this);
				int previousIndex = (indexOfThis + nations.Count() - 1) % nations.Count();
				return nations.ElementAt(previousIndex);
			}
		}

	}
}
