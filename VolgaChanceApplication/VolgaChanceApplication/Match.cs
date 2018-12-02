using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class Match
	{
		public Club HomeClub { get; private set; }
		public Club AwayClub { get; private set; }

		public HomeClubChance Chance { get; private set; }

		public Match(Club home, Club away, HomeClubChance chance)
		{
			HomeClub = home;
			AwayClub = away;
			Chance = chance;
		}

		public List<MatchOutcome> AllOutcome
		{
			get
			{
				return new List<MatchOutcome>
					{
						new MatchOutcome(this, Outcome.Win),
						new MatchOutcome(this, Outcome.Draw),
						new MatchOutcome(this, Outcome.Lose)
					};
			}
		}

		public static Match[] Matches = new[]
			{
				new Match(Club.Amkar, Club.Zenit, new HomeClubChance(3.2, 3.25, 2.2)), 
				new Match(Club.Dinamo, Club.Volga, new HomeClubChance(1.55, 3.75, 6.0)), 
				new Match(Club.Terek, Club.KryljaSovetov, new HomeClubChance(2.5, 3, 2.87)), 
				new Match(Club.Rostov, Club.CSKA, new HomeClubChance(2.2, 3.25, 2.75)), 
			};
	}
}
