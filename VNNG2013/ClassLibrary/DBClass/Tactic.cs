using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;
using ClassLibrary.Resources.Tactics;
using ClassLibrary.Resources.Tactics.Classes;

namespace ClassLibrary.DBClass
{
	[Entity("Тактика", false)]
	public class Tactic : DBObject
	{
		[DBAttribute]
		private string name;
		public string Name
		{
			get { return this.name; }
			set 
			{ 
				this.name = value;
				this.ToNotifyChanges("Name");
				this.ToNotifyChanges("DisplayedText");
			}
		}

		[DBAttribute]
		internal id ManagerID;
		private Person manager;
		public Person Manager
		{
			get { return this.GetDBObjectPropertyValue<Person>("Manager"); }
			set { this.SetDBObjectPropertyValue("Manager", value); }
		}

		/// <summary> Возвращает местоположение полевых игроков на поле согластно заданной тактике </summary>
		[Sorting("Number")]
		public PositionInfo[] PositionInfoes
		{
			get { return this.GetBindedArrayPropertyValue<PositionInfo>("PositionInfoes"); }
			set { this.SetBindedArrayPropertyValue("PositionInfoes", value); }
		}


		public Tactic() : base() { }

		public override string DisplayedText
		{
			get
			{
				return this.Name;
			}
		}



	}
}
