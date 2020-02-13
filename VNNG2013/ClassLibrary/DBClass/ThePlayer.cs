using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	[Entity("Игрок в игру VNNG2013", false)]
	public class ThePlayer : DBObject
	{
		[DBAttribute]
		internal id PlayedManagerID;
		private Person playedManager;
		public Person PlayedManager
		{
			get { return this.GetDBObjectPropertyValue<Person>("PlayedManager"); }
			set { this.SetDBObjectPropertyValue("PlayedManager", value); }
		}



		public ThePlayer() : base() { }

		public override string DisplayedText
		{
			get { return string.Empty; }
		}
	}
}
