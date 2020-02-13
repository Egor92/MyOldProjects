using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	[Entity("Стадион", true)]
	[ItemsCollection("Информация")]
	public class Stadium : DBObject
	{
		private string name;
		[DBAttribute]
		[EditingProperty("Название стадиона")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		private int capacity;
		[DBAttribute]
		[EditingProperty("Количество посадочных мест")]
		[Limit(IntegerLimit.PositiveOrZero)]
		public int Capacity
		{
			get { return this.capacity; }
			set { this.capacity = value; this.ToNotifyChanges("Capacity"); }
		}

		[DBAttribute]
		internal id CityID;
		private City city;
		[EditingProperty("Город")]
		public City City
		{
			get { return this.GetDBObjectPropertyValue<City>("City"); }
			set { this.SetDBObjectPropertyValue("City", value); }
		}




		public Stadium() : base() { }

		protected override void EmptyInitialization()
		{
			this.Name = "Новый стадион";
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


	}
}
