using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ClassLibrary.Attributes;

namespace ClassLibrary.Resources.Tactics.Classes
{
	public class PlayerPlacement
	{
		private int determinant;
		public int Determinant
		{
			get { return this.determinant; }
		}

		private int number;
		public int Number
		{
			get { return this.number; }
		}

		private int locationKey;
		public int LocationKey
		{
			get { return this.locationKey; }
		}

		public PlayerPlacement(int determinant, int number, int locationKey)
		{
			this.determinant = determinant;
			this.number = number;
			this.locationKey = locationKey;
		}

		public Point GetLocation()
		{
			return StandartTactics.PlayerPlacementLocations.Single(x => x.Key == this.LocationKey).Location;
		}
	}
}
