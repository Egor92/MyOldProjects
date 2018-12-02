using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class Club
	{
		public string Name { get; private set; }
		public Statistic StartStatistic { get; private set; }

		private Club(string name, Statistic startStatistic)
		{
			Name = name;
			StartStatistic = startStatistic;
		}

		public static readonly Club CSKA = new Club("ЦСКА", new Statistic(64, 20));
		public static readonly Club Zenit = new Club("Зенит", new Statistic(61, 18));
		public static readonly Club Dinamo = new Club("Динамо", new Statistic(47, 14));
		public static readonly Club Terek = new Club("Терек", new Statistic(45, 13));
		public static readonly Club Amkar = new Club("Амкар", new Statistic(28, 7));
		public static readonly Club Volga = new Club("ВОЛГА", new Statistic(28, 7));
		public static readonly Club KryljaSovetov = new Club("Крылья Советов", new Statistic(28, 7));
		public static readonly Club Rostov = new Club("Ростов", new Statistic(26, 6));

		public static Club[] AllClubs = new[]
			{
				CSKA, Zenit, Dinamo, Terek, Amkar, Volga, KryljaSovetov, Rostov
			};

		public static Club[] RivalClubs = new[]
			{
				Amkar, Volga, KryljaSovetov, Rostov
			};

		public override string ToString()
		{
			return Name;
		}
	}
}
