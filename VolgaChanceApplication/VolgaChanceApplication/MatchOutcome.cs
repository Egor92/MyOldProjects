using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class MatchOutcome
	{
		public Dictionary<Club, Outcome> ClubMatchPoints { get; private set; }
		public double Probability { get; private set; }

		public MatchOutcome(Match match, Outcome outcome)
		{
			ClubMatchPoints = new Dictionary<Club, Outcome>();
			switch (outcome)
			{
				case Outcome.Win:
					ClubMatchPoints.Add(match.HomeClub, Outcome.Win);
					ClubMatchPoints.Add(match.AwayClub, Outcome.Lose);
					Probability = match.Chance.Win;
					break;
				case Outcome.Draw:
					ClubMatchPoints.Add(match.HomeClub, Outcome.Draw);
					ClubMatchPoints.Add(match.AwayClub, Outcome.Draw);
					Probability = match.Chance.Draw;
					break;
				case Outcome.Lose:
					ClubMatchPoints.Add(match.HomeClub, Outcome.Lose);
					ClubMatchPoints.Add(match.AwayClub, Outcome.Win);
					Probability = match.Chance.Lose;
					break;
			}
		}
	}

	public enum Outcome
	{
		Win=3, Draw=1, Lose=0
	}

	public static class Helper
	{
		public static string GetRussianName(this Outcome outcome)
		{
			switch (outcome)
			{
				case Outcome.Win:
					return "победа";
				case Outcome.Draw:
					return "ничья";
				case Outcome.Lose:
					return "поражение";
				default:
					throw new Exception();
			}
		}
	}

}
