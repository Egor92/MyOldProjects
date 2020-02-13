using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	[Entity("Город", true)]
	[ItemsCollection("Информация")]
	public class City : DBObject
	{
		private string name;
		[DBAttribute]
		[EditingProperty("Название")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		private int population;
		[DBAttribute]
		[EditingProperty("Численность населения")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int Population
		{
			get { return this.population; }
			set { this.population = value; this.ToNotifyChanges("Population"); }
		}

		private int reputation;
		[DBAttribute]
		[EditingProperty("Уровень привлекательности")]
		[Limit(IntegerLimit.RatingLarge)]
		public int Reputation
		{
			get { return this.reputation; }
			set { this.reputation = value; this.ToNotifyChanges("Reputation"); }
		}

		[DBAttribute]
		internal id NationID;
		private Nation nation;
		[EditingProperty("Страна")]
		public Nation Nation
		{
			get { return this.GetDBObjectPropertyValue<Nation>("Nation"); }
			set { this.SetDBObjectPropertyValue("Nation", value); }
		}

		public City() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();
			this.Name = "Новый город";
		}

		public override string DisplayedText 
		{ 
			get
			{
				return string.Format(
					"{0} ({1})",
					this.Name,
					this.Nation != null ? this.Nation.Name : "?");
			}
		}
	}
}
