using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class Statistic
	{
		public int Points;
		public int WinsCount;
		public int Priority;

		public Statistic(int points, int winsCount)
		{
			Points = points;
			WinsCount = winsCount;
		}
		public Statistic(Statistic statistic)
		{
			Points = statistic.Points;
			WinsCount = statistic.WinsCount;
			Priority = statistic.Priority;
		}

		public override string ToString()
		{
			return string.Format("points: {0}; priority: {1}", Points, Priority);
		}
	}
}
