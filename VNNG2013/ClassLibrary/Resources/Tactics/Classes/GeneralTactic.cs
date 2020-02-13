using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ClassLibrary.Attributes;
using ClassLibrary.DBClass;

namespace ClassLibrary.Resources.Tactics.Classes
{
	public class GeneralTactic
	{
		private int defendersCount;
		public int DefendersCount
		{
			get { return this.defendersCount; }
		}

		private int midfieldersCount;
		public int MidfieldersCount
		{
			get { return this.midfieldersCount; }
		}

		private int strikersCount;
		public int StrikersCount
		{
			get { return this.strikersCount; }
		}

		public string Name
		{
			get
			{
				return string.Format("{0}-{1}-{2}", this.DefendersCount, this.MidfieldersCount, this.StrikersCount);
			}
		}

		public GeneralTactic(int defendersCount, int midfieldersCount, int strikersCount): base()
		{
			if (defendersCount + midfieldersCount + strikersCount != 10)
				throw new ArgumentException("Сумма входных аргументов должна быть равна 10");
			this.defendersCount = defendersCount;
			this.midfieldersCount = midfieldersCount;
			this.strikersCount = strikersCount;
		}

		public List<Point> GetLocations(int defFormationKey, int midFormationKey, int strFormationKey)
		{
			List<Point> Result = new List<Point>();
			Result.Add(new Point(340, 950));
			Result.AddRange(StandartTactics.LineFormations.Single(x => x.LineKey == 1 && x.PlayersCount == this.DefendersCount && x.Formation == defFormationKey).GetLocations());
			Result.AddRange(StandartTactics.LineFormations.Single(x => x.LineKey == 2 && x.PlayersCount == this.MidfieldersCount && x.Formation == midFormationKey).GetLocations());
			Result.AddRange(StandartTactics.LineFormations.Single(x => x.LineKey == 3 && x.PlayersCount == this.StrikersCount && x.Formation == strFormationKey).GetLocations());
			return Result;
		}

		public static GeneralTactic GetByKey(int key)
		{
			return StandartTactics.GeneralTactics[key];
		}

		public List<LineFormation> DefFormations
		{
			get
			{
				return StandartTactics.LineFormations.Where(x => x.LineKey == 1 && x.PlayersCount == this.DefendersCount).ToList();
			}
		}

		public List<LineFormation> MidFormations
		{
			get
			{
				return StandartTactics.LineFormations.Where(x => x.LineKey == 2 && x.PlayersCount == this.MidfieldersCount).ToList();
			}
		}

		public List<LineFormation> StrFormations
		{
			get
			{
				return StandartTactics.LineFormations.Where(x => x.LineKey == 3 && x.PlayersCount == this.StrikersCount).ToList();
			}
		}
	}
}
