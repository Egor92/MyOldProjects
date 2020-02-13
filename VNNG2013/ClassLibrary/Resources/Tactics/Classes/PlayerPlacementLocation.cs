using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ClassLibrary.Attributes;

namespace ClassLibrary.Resources.Tactics.Classes
{
	public class PlayerPlacementLocation
	{
		private int key;
		public int Key
		{
			get { return this.key; }
		}

		private Point location;
		public Point Location
		{
			get { return this.location; }
		}

		public PlayerPlacementLocation(int key, int x, int y)
		{
			this.key = key;
			this.location = new Point(y, x);
		}

		public Point GetLocation()
		{
			return this.Location;
		}
	}
}
