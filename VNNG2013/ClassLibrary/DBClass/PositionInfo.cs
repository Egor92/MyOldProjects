using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	[Entity("Информация о позиции игрока на поле", false)]
	public class PositionInfo : DBObject
	{
		[DBAttribute]
		internal id TacticID;
		private Tactic tactic;
		public Tactic Tactic
		{
			get { return this.GetDBObjectPropertyValue<Tactic>("Tactic"); }
			set { this.SetDBObjectPropertyValue("Tactic", value); }
		}

		[DBAttribute]
		private int number;
		public int Number
		{
			get { return this.number; }
			set { this.number = value; this.ToNotifyChanges("Number"); }
		}

		[DBAttribute]
		private double x;
		public double X
		{
			get { return this.x; }
			set { this.x = value; this.ToNotifyChanges("X"); }
		}

		private double y;
		[DBAttribute]
		public double Y
		{
			get { return this.y; }
			set { this.y = value; this.ToNotifyChanges("Y"); }
		}

		public PositionInfo() : base() { }

		public Point Location
		{
			get { return new Point(this.X, this.Y); }
		}
	}
}
