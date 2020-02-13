using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ClassLibrary.Attributes;

namespace ClassLibrary.Resources.Tactics.Classes
{
	public class LineFormation
	{
		private int lineKey;
		public int LineKey
		{
			get { return this.lineKey; }
		}

		private int playersCount;
		public int PlayersCount
		{
			get { return this.playersCount; }
		}

		private int formation;
		public int Formation
		{
			get { return this.formation; }
		}

		private string text;
		public string Text
		{
			get { return this.text; }
		}

		public LineFormation(int lineKey, int playersCount, int formation, string text)
		{
			this.lineKey = lineKey;
			this.playersCount = playersCount;
			this.formation = formation;
			this.text = text;
		}

		public int Determinant
		{
			get { return this.lineKey * 100 + this.playersCount * 10 + this.formation; }
		}

		public List<Point> GetLocations()
		{
			return StandartTactics.PlayerPlacements.Where(x => x.Determinant == this.Determinant).Select(x => x.GetLocation()).ToList();
		}

	}
}
