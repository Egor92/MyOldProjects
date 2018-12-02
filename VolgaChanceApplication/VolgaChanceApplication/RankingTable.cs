using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class RankingTable
	{
		public List<KeyValuePair<Club, Statistic>> Table { get; private set; }
		public double Probability { get; private set; }
		private readonly StreamWriter _writer;
		private readonly MatchOutcome[] _outcomes;

		public RankingTable(StreamWriter writer, params MatchOutcome[] outcomes)
		{
			_writer = writer;
			_outcomes = outcomes;

			Probability = 1.0;
			foreach (var probability in _outcomes.Select(x => x.Probability))
				Probability *= probability;

			Table = new List<KeyValuePair<Club, Statistic>>
				{
					new KeyValuePair<Club, Statistic>(Club.CSKA, new Statistic(Club.CSKA.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Zenit, new Statistic(Club.Zenit.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Dinamo, new Statistic(Club.Dinamo.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Terek, new Statistic(Club.Terek.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Amkar, new Statistic(Club.Amkar.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Volga, new Statistic(Club.Volga.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.KryljaSovetov, new Statistic(Club.KryljaSovetov.StartStatistic)),
					new KeyValuePair<Club, Statistic>(Club.Rostov, new Statistic(Club.Rostov.StartStatistic))
				};


			foreach (var outcome in _outcomes.SelectMany(x => x.ClubMatchPoints))
			{
				if (outcome.Value == Outcome.Lose) continue;
				Table.Single(x => x.Key == outcome.Key).Value.Points += (int)outcome.Value;
			}

			Order();
		}

		private void Order()
		{
			var amkarStatistic = Table.Single(x => x.Key == Club.Amkar);
			var volgaStatistic = Table.Single(x => x.Key == Club.Volga);
			var kryljaSovetovStatistic = Table.Single(x => x.Key == Club.KryljaSovetov);
			var rostovStatistic = Table.Single(x => x.Key == Club.Rostov);
			if (EqualsPoints(amkarStatistic, volgaStatistic, kryljaSovetovStatistic, rostovStatistic))
			{
				//It's impossible
				return;
			}
			if (EqualsPoints(amkarStatistic, volgaStatistic, kryljaSovetovStatistic))
			{
				amkarStatistic.Value.Priority = 3;
				volgaStatistic.Value.Priority = 2;
				kryljaSovetovStatistic.Value.Priority = 1;
			}
			else if (EqualsPoints(amkarStatistic, volgaStatistic, rostovStatistic))
			{
				amkarStatistic.Value.Priority = 3;
				volgaStatistic.Value.Priority = 2;
				rostovStatistic.Value.Priority = 1;
			}
			else if (EqualsPoints(amkarStatistic, kryljaSovetovStatistic, rostovStatistic))
			{
				rostovStatistic.Value.Priority = 3;
				kryljaSovetovStatistic.Value.Priority = 2;
				amkarStatistic.Value.Priority = 1;
			}
			else if (EqualsPoints(volgaStatistic, kryljaSovetovStatistic, rostovStatistic))
			{
				volgaStatistic.Value.Priority = 3;
				kryljaSovetovStatistic.Value.Priority = 2;
				amkarStatistic.Value.Priority = 1;
			}
			else
			{
				if (EqualsPoints(amkarStatistic, volgaStatistic))
				{
					amkarStatistic.Value.Priority = 3;
					volgaStatistic.Value.Priority = 2;
				}
				if (EqualsPoints(amkarStatistic, kryljaSovetovStatistic))
				{
					amkarStatistic.Value.Priority = 3;
					kryljaSovetovStatistic.Value.Priority = 2;
				}
				if (EqualsPoints(amkarStatistic, rostovStatistic))
				{
					amkarStatistic.Value.Priority = 2;
					rostovStatistic.Value.Priority = 3;
				}
				if (EqualsPoints(volgaStatistic, kryljaSovetovStatistic))
				{
					volgaStatistic.Value.Priority = 3;
					kryljaSovetovStatistic.Value.Priority = 2;
				}
				if (EqualsPoints(volgaStatistic, rostovStatistic))
				{
					volgaStatistic.Value.Priority = 3;
					rostovStatistic.Value.Priority = 2;
				}
				if (EqualsPoints(kryljaSovetovStatistic, rostovStatistic))
				{
					kryljaSovetovStatistic.Value.Priority = 3;
					rostovStatistic.Value.Priority = 2;
				}
			}
			Table = Table.OrderByDescending(x => x.Value.Points).ThenByDescending(x => x.Value.Priority).ToList();
		}

		private static bool EqualsPoints(params KeyValuePair<Club, Statistic>[] clubPoints)
		{
			for (int i = 0; i < clubPoints.Length; i++)
				for (int j = i + 1; j < clubPoints.Length; j++)
					if (clubPoints[i].Value.Points != clubPoints[j].Value.Points)
						return false;
			return true;
		}

		public bool IsRemainClubInPremierLeague(int index)
		{
			return Table.IndexOf(Table.Single(x => x.Key == Club.RivalClubs[index])) < 6;
		}

		private void PrintRivalMatchesOutcomes()
		{
			_writer.WriteLine("Исходы матчей:");
			_writer.WriteLine("\t{0} - {1}", Club.Amkar, _outcomes.SelectMany(x => x.ClubMatchPoints).Single(x => x.Key == Club.Amkar).Value.GetRussianName());
			_writer.WriteLine("\t{0} - {1}", Club.Volga, _outcomes.SelectMany(x => x.ClubMatchPoints).Single(x => x.Key == Club.Volga).Value.GetRussianName());
			_writer.WriteLine("\t{0} - {1}", Club.KryljaSovetov, _outcomes.SelectMany(x => x.ClubMatchPoints).Single(x => x.Key == Club.KryljaSovetov).Value.GetRussianName());
			_writer.WriteLine("\t{0} - {1}", Club.Rostov, _outcomes.SelectMany(x => x.ClubMatchPoints).Single(x => x.Key == Club.Rostov).Value.GetRussianName());
		}

		public void PrintTable(int counter)
		{
			_writer.WriteLine("Результат #{0}:", counter);
			_writer.WriteLine("Вероятность = {0}%", (Probability * 100).ToString("F2"));
			PrintRivalMatchesOutcomes();
			_writer.WriteLine("Таблица:");
			foreach (var clubStatistic in Table.Where(x => x.Value.Points < 40))
				_writer.WriteLine("\t{0}: {1}", clubStatistic.Value.Points, clubStatistic.Key.Name);
			_writer.WriteLine("**************************************************************");
			_writer.WriteLine();
		}
	}
}
