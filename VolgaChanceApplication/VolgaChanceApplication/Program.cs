using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VolgaChanceApplication
{
	class Program
	{
		private static void Main(string[] args)
		{
			VolgaChances();
		}

		private static void VolgaChances()
		{
			using (var writer = new StreamWriter(string.Format("Шансы Волги при различных исходах её встречи.txt")))
			{
				var goodChances = new double[3];
				var badChances = new double[3];

				writer.WriteLine("Матчи:");
				foreach (var match in Match.Matches)
					writer.WriteLine("{0} - {1}\n\tКоэффициенты (по championat.com): ({2}% - {3}% - {4}%)",
									  match.HomeClub.Name,
									  match.AwayClub.Name,
									  (match.Chance.Win * 100).ToString("F2"),
									  (match.Chance.Draw * 100).ToString("F2"),
									  (match.Chance.Lose * 100).ToString("F2"));
				writer.WriteLine();

				int counter = 1;
				var volgaOutcomes = Match.Matches[1].AllOutcome;
				for (int j = 0; j < volgaOutcomes.Count; j++)
					foreach (var outcome0 in Match.Matches[0].AllOutcome)
						foreach (var outcome2 in Match.Matches[2].AllOutcome)
							foreach (var outcome3 in Match.Matches[3].AllOutcome)
							{
								var rankingTable = new RankingTable(writer, outcome0, volgaOutcomes[j], outcome2, outcome3);
								rankingTable.PrintTable(counter++);

								if (rankingTable.IsRemainClubInPremierLeague(1))
									goodChances[j] += rankingTable.Probability;
								else
									badChances[j] += rankingTable.Probability;
							}

				for (int j = 0; j < volgaOutcomes.Count; j++)
				{
					writer.WriteLine("{0}:", Club.Volga);
					writer.WriteLine("\tШансы избежать стыков: {0}%", (goodChances[j] * 100).ToString("F2"));
					writer.WriteLine("\tШансы попасть в стыки: {0}%", (badChances[j] * 100).ToString("F2"));
				}
				Console.WriteLine("OK!");
				Console.ReadKey();
			}
		}

		private static void GeneralChances()
		{
			using (var writer = new StreamWriter(string.Format("Шансы команд избежать стыков.txt")))
			{
				var goodChances = new double[Club.RivalClubs.Length];
				var badChances = new double[Club.RivalClubs.Length];

				writer.WriteLine("Матчи:");
				foreach (var match in Match.Matches)
					writer.WriteLine("{0} - {1}\n\tКоэффициенты (по championat.com): ({2}% - {3}% - {4}%)",
									  match.HomeClub.Name,
									  match.AwayClub.Name,
									  (match.Chance.Win * 100).ToString("F2"),
									  (match.Chance.Draw * 100).ToString("F2"),
									  (match.Chance.Lose * 100).ToString("F2"));
				writer.WriteLine();

				int counter = 1;
				foreach (var outcome0 in Match.Matches[0].AllOutcome)
					foreach (var outcome1 in Match.Matches[1].AllOutcome)
						foreach (var outcome2 in Match.Matches[2].AllOutcome)
							foreach (var outcome3 in Match.Matches[3].AllOutcome)
							{
								var rankingTable = new RankingTable(writer, outcome0, outcome1, outcome2, outcome3);
								rankingTable.PrintTable(counter++);

								for (int i = 0; i < Club.RivalClubs.Length; i++)
									if (rankingTable.IsRemainClubInPremierLeague(i))
										goodChances[i] += rankingTable.Probability;
									else
										badChances[i] += rankingTable.Probability;
							}

				for (int i = 0; i < Club.RivalClubs.Length; i++)
				{
					writer.WriteLine("{0}:", Club.RivalClubs[i]);
					writer.WriteLine("\tШансы избежать стыков: {0}%", (goodChances[i] * 100).ToString("F2"));
					writer.WriteLine("\tШансы попасть в стыки: {0}%", (badChances[i] * 100).ToString("F2"));
				}
				Console.WriteLine("OK!");
				Console.ReadKey();
			}
		}
	}
}
