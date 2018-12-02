using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class HomeClubChance
	{
		public double WinCoef { get; private set; }
		public double DrawCoef { get; private set; }
		public double LoseCoef { get; private set; }
		public double Win { get; private set; }
		public double Draw { get; private set; }
		public double Lose { get; private set; }

		public HomeClubChance(double winCoef, double drawCoef, double loseCoef)
		{
			WinCoef = winCoef;
			DrawCoef = drawCoef;
			LoseCoef = loseCoef;

			double total = 1.0 / winCoef + 1.0 / drawCoef + 1.0 / loseCoef;
			Win = 1.0 / winCoef / total;
			Draw = 1.0 / drawCoef / total;
			Lose = 1.0 / loseCoef / total;
		}
	}
}
