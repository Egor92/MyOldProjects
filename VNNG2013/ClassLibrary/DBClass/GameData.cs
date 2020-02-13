using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;

namespace ClassLibrary.DBClass
{
	[Entity("Данные об игре", false)]
	class GameData : DBObject
	{
		private DateTime currentDate;
		[DBAttribute]
		public DateTime CurrentDate
		{
			get { return this.currentDate; }
			set { this.currentDate = value; this.ToNotifyChanges("CurrentDate"); }
		}

		public GameData() : base() { }

		protected override void EmptyInitialization()
		{
			this.CurrentDate = new DateTime(2012, 1, 1);
		}

		public override string DisplayedText
		{
			get { return string.Empty; }
		}
	}
	
}
