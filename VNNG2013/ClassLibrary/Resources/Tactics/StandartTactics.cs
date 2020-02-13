using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Resources.Tactics.Classes;

namespace ClassLibrary.Resources.Tactics
{
	public static class StandartTactics
	{
		private static List<GeneralTactic> generalTactics = new List<GeneralTactic>();
		public static List<GeneralTactic> GeneralTactics
		{
			get { return generalTactics; }
		}

		private static List<LineFormation> lineTactics = new List<LineFormation>();
		public static List<LineFormation> LineFormations
		{
			get { return StandartTactics.lineTactics; }
		}

		private static List<PlayerPlacement> playerPlacements = new List<PlayerPlacement>();
		public static List<PlayerPlacement> PlayerPlacements
		{
			get { return StandartTactics.playerPlacements; }
		}

		private static List<PlayerPlacementLocation> playerPlacementLocations = new List<PlayerPlacementLocation>();
		public static List<PlayerPlacementLocation> PlayerPlacementLocations
		{
			get { return StandartTactics.playerPlacementLocations; }
		}

		static StandartTactics()
		{
			#region Заполнение данными
			generalTactics.Add(new GeneralTactic(5, 4, 1));
			generalTactics.Add(new GeneralTactic(5, 3, 2));
			generalTactics.Add(new GeneralTactic(4, 5, 1));
			generalTactics.Add(new GeneralTactic(4, 4, 2));
			generalTactics.Add(new GeneralTactic(4, 3, 3));
			generalTactics.Add(new GeneralTactic(3, 5, 2));
			generalTactics.Add(new GeneralTactic(3, 4, 3));


			lineTactics.Add(new LineFormation(1, 3, 0, "В линию"));
			lineTactics.Add(new LineFormation(1, 3, 1, "По всей длине"));
			lineTactics.Add(new LineFormation(1, 4, 0, "В линию"));
			lineTactics.Add(new LineFormation(1, 4, 1, "В линию + активные фланги"));
			lineTactics.Add(new LineFormation(1, 5, 0, "В линию"));
			lineTactics.Add(new LineFormation(1, 5, 1, "В линию + активные фланги"));
			lineTactics.Add(new LineFormation(1, 5, 2, "Либеро"));
			lineTactics.Add(new LineFormation(2, 3, 0, "В линию"));
			lineTactics.Add(new LineFormation(2, 3, 1, "По всей длине"));
			lineTactics.Add(new LineFormation(2, 3, 2, "Треугольник 1"));
			lineTactics.Add(new LineFormation(2, 3, 3, "Треугольник 2"));
			lineTactics.Add(new LineFormation(2, 4, 0, "В линию"));
			lineTactics.Add(new LineFormation(2, 4, 1, "Активные фланги"));
			lineTactics.Add(new LineFormation(2, 4, 2, "Ромб"));
			lineTactics.Add(new LineFormation(2, 4, 3, "Квадрат"));
			lineTactics.Add(new LineFormation(2, 5, 0, "В линию"));
			lineTactics.Add(new LineFormation(2, 5, 1, "В линию + активные фланги"));
			lineTactics.Add(new LineFormation(2, 5, 2, "Расстановка M"));
			lineTactics.Add(new LineFormation(2, 5, 3, "Расстановка W"));
			lineTactics.Add(new LineFormation(3, 1, 0, "В линию"));
			lineTactics.Add(new LineFormation(3, 2, 0, "В линию"));
			lineTactics.Add(new LineFormation(3, 2, 1, "Левый остроатакующий"));
			lineTactics.Add(new LineFormation(3, 2, 2, "Правый остроатакующий"));
			lineTactics.Add(new LineFormation(3, 3, 0, "В линию"));
			lineTactics.Add(new LineFormation(3, 3, 1, "Остроатакующий"));
			lineTactics.Add(new LineFormation(3, 3, 2, "Оттянутый назад"));
			lineTactics.Add(new LineFormation(3, 3, 3, "По всей длине"));


			playerPlacements.Add(new PlayerPlacement(130, 1, 5));
			playerPlacements.Add(new PlayerPlacement(130, 2, 6));
			playerPlacements.Add(new PlayerPlacement(130, 3, 7));
			playerPlacements.Add(new PlayerPlacement(131, 1, 1));
			playerPlacements.Add(new PlayerPlacement(131, 2, 6));
			playerPlacements.Add(new PlayerPlacement(131, 3, 4));
			playerPlacements.Add(new PlayerPlacement(140, 1, 1));
			playerPlacements.Add(new PlayerPlacement(140, 2, 2));
			playerPlacements.Add(new PlayerPlacement(140, 3, 3));
			playerPlacements.Add(new PlayerPlacement(140, 4, 4));
			playerPlacements.Add(new PlayerPlacement(141, 1, 27));
			playerPlacements.Add(new PlayerPlacement(141, 2, 2));
			playerPlacements.Add(new PlayerPlacement(141, 3, 3));
			playerPlacements.Add(new PlayerPlacement(141, 4, 28));
			playerPlacements.Add(new PlayerPlacement(150, 1, 1));
			playerPlacements.Add(new PlayerPlacement(150, 2, 8));
			playerPlacements.Add(new PlayerPlacement(150, 3, 6));
			playerPlacements.Add(new PlayerPlacement(150, 4, 9));
			playerPlacements.Add(new PlayerPlacement(150, 5, 4));
			playerPlacements.Add(new PlayerPlacement(151, 1, 27));
			playerPlacements.Add(new PlayerPlacement(151, 2, 8));
			playerPlacements.Add(new PlayerPlacement(151, 3, 6));
			playerPlacements.Add(new PlayerPlacement(151, 4, 9));
			playerPlacements.Add(new PlayerPlacement(151, 5, 28));
			playerPlacements.Add(new PlayerPlacement(152, 1, 1));
			playerPlacements.Add(new PlayerPlacement(152, 2, 2));
			playerPlacements.Add(new PlayerPlacement(152, 3, 40));
			playerPlacements.Add(new PlayerPlacement(152, 4, 3));
			playerPlacements.Add(new PlayerPlacement(152, 5, 4));
			playerPlacements.Add(new PlayerPlacement(230, 1, 14));
			playerPlacements.Add(new PlayerPlacement(230, 2, 15));
			playerPlacements.Add(new PlayerPlacement(230, 3, 16));
			playerPlacements.Add(new PlayerPlacement(231, 1, 10));
			playerPlacements.Add(new PlayerPlacement(231, 2, 15));
			playerPlacements.Add(new PlayerPlacement(231, 3, 13));
			playerPlacements.Add(new PlayerPlacement(232, 1, 20));
			playerPlacements.Add(new PlayerPlacement(232, 2, 24));
			playerPlacements.Add(new PlayerPlacement(232, 3, 21));
			playerPlacements.Add(new PlayerPlacement(233, 1, 23));
			playerPlacements.Add(new PlayerPlacement(233, 2, 19));
			playerPlacements.Add(new PlayerPlacement(233, 3, 25));
			playerPlacements.Add(new PlayerPlacement(240, 1, 10));
			playerPlacements.Add(new PlayerPlacement(240, 2, 11));
			playerPlacements.Add(new PlayerPlacement(240, 3, 12));
			playerPlacements.Add(new PlayerPlacement(240, 4, 13));
			playerPlacements.Add(new PlayerPlacement(241, 1, 22));
			playerPlacements.Add(new PlayerPlacement(241, 2, 11));
			playerPlacements.Add(new PlayerPlacement(241, 3, 12));
			playerPlacements.Add(new PlayerPlacement(241, 4, 26));
			playerPlacements.Add(new PlayerPlacement(242, 1, 10));
			playerPlacements.Add(new PlayerPlacement(242, 2, 19));
			playerPlacements.Add(new PlayerPlacement(242, 3, 24));
			playerPlacements.Add(new PlayerPlacement(242, 4, 13));
			playerPlacements.Add(new PlayerPlacement(243, 1, 20));
			playerPlacements.Add(new PlayerPlacement(243, 2, 21));
			playerPlacements.Add(new PlayerPlacement(243, 3, 23));
			playerPlacements.Add(new PlayerPlacement(243, 4, 25));
			playerPlacements.Add(new PlayerPlacement(250, 1, 10));
			playerPlacements.Add(new PlayerPlacement(250, 2, 17));
			playerPlacements.Add(new PlayerPlacement(250, 3, 15));
			playerPlacements.Add(new PlayerPlacement(250, 4, 18));
			playerPlacements.Add(new PlayerPlacement(250, 5, 13));
			playerPlacements.Add(new PlayerPlacement(251, 1, 22));
			playerPlacements.Add(new PlayerPlacement(251, 2, 17));
			playerPlacements.Add(new PlayerPlacement(251, 3, 15));
			playerPlacements.Add(new PlayerPlacement(251, 4, 18));
			playerPlacements.Add(new PlayerPlacement(251, 5, 26));
			playerPlacements.Add(new PlayerPlacement(252, 1, 10));
			playerPlacements.Add(new PlayerPlacement(252, 2, 23));
			playerPlacements.Add(new PlayerPlacement(252, 3, 19));
			playerPlacements.Add(new PlayerPlacement(252, 4, 25));
			playerPlacements.Add(new PlayerPlacement(252, 5, 13));
			playerPlacements.Add(new PlayerPlacement(253, 1, 22));
			playerPlacements.Add(new PlayerPlacement(253, 2, 20));
			playerPlacements.Add(new PlayerPlacement(253, 3, 24));
			playerPlacements.Add(new PlayerPlacement(253, 4, 21));
			playerPlacements.Add(new PlayerPlacement(253, 5, 26));
			playerPlacements.Add(new PlayerPlacement(310, 1, 33));
			playerPlacements.Add(new PlayerPlacement(320, 1, 32));
			playerPlacements.Add(new PlayerPlacement(320, 2, 34));
			playerPlacements.Add(new PlayerPlacement(321, 1, 35));
			playerPlacements.Add(new PlayerPlacement(321, 2, 31));
			playerPlacements.Add(new PlayerPlacement(322, 1, 29));
			playerPlacements.Add(new PlayerPlacement(322, 2, 37));
			playerPlacements.Add(new PlayerPlacement(330, 1, 32));
			playerPlacements.Add(new PlayerPlacement(330, 2, 33));
			playerPlacements.Add(new PlayerPlacement(330, 3, 34));
			playerPlacements.Add(new PlayerPlacement(331, 1, 32));
			playerPlacements.Add(new PlayerPlacement(331, 2, 36));
			playerPlacements.Add(new PlayerPlacement(331, 3, 34));
			playerPlacements.Add(new PlayerPlacement(332, 1, 32));
			playerPlacements.Add(new PlayerPlacement(332, 2, 30));
			playerPlacements.Add(new PlayerPlacement(332, 3, 34));
			playerPlacements.Add(new PlayerPlacement(333, 1, 38));
			playerPlacements.Add(new PlayerPlacement(333, 2, 33));
			playerPlacements.Add(new PlayerPlacement(333, 3, 39));


			playerPlacementLocations.Add(new PlayerPlacementLocation(1, 780, 100));
			playerPlacementLocations.Add(new PlayerPlacementLocation(2, 780, 260));
			playerPlacementLocations.Add(new PlayerPlacementLocation(3, 780, 420));
			playerPlacementLocations.Add(new PlayerPlacementLocation(4, 780, 580));
			playerPlacementLocations.Add(new PlayerPlacementLocation(5, 780, 180));
			playerPlacementLocations.Add(new PlayerPlacementLocation(6, 780, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(7, 780, 500));
			playerPlacementLocations.Add(new PlayerPlacementLocation(8, 780, 220));
			playerPlacementLocations.Add(new PlayerPlacementLocation(9, 780, 460));
			playerPlacementLocations.Add(new PlayerPlacementLocation(10, 500, 100));
			playerPlacementLocations.Add(new PlayerPlacementLocation(11, 500, 260));
			playerPlacementLocations.Add(new PlayerPlacementLocation(12, 500, 420));
			playerPlacementLocations.Add(new PlayerPlacementLocation(13, 500, 580));
			playerPlacementLocations.Add(new PlayerPlacementLocation(14, 500, 180));
			playerPlacementLocations.Add(new PlayerPlacementLocation(15, 500, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(16, 500, 500));
			playerPlacementLocations.Add(new PlayerPlacementLocation(17, 500, 220));
			playerPlacementLocations.Add(new PlayerPlacementLocation(18, 500, 460));
			playerPlacementLocations.Add(new PlayerPlacementLocation(19, 600, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(20, 600, 220));
			playerPlacementLocations.Add(new PlayerPlacementLocation(21, 600, 460));
			playerPlacementLocations.Add(new PlayerPlacementLocation(22, 400, 100));
			playerPlacementLocations.Add(new PlayerPlacementLocation(23, 400, 220));
			playerPlacementLocations.Add(new PlayerPlacementLocation(24, 400, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(25, 400, 460));
			playerPlacementLocations.Add(new PlayerPlacementLocation(26, 400, 580));
			playerPlacementLocations.Add(new PlayerPlacementLocation(27, 720, 100));
			playerPlacementLocations.Add(new PlayerPlacementLocation(28, 720, 580));
			playerPlacementLocations.Add(new PlayerPlacementLocation(29, 280, 180));
			playerPlacementLocations.Add(new PlayerPlacementLocation(30, 280, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(31, 280, 500));
			playerPlacementLocations.Add(new PlayerPlacementLocation(32, 220, 180));
			playerPlacementLocations.Add(new PlayerPlacementLocation(33, 220, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(34, 220, 500));
			playerPlacementLocations.Add(new PlayerPlacementLocation(35, 160, 180));
			playerPlacementLocations.Add(new PlayerPlacementLocation(36, 160, 340));
			playerPlacementLocations.Add(new PlayerPlacementLocation(37, 160, 500));
			playerPlacementLocations.Add(new PlayerPlacementLocation(38, 220, 100));
			playerPlacementLocations.Add(new PlayerPlacementLocation(39, 220, 580));
			playerPlacementLocations.Add(new PlayerPlacementLocation(40, 840, 340));
			#endregion
		}
	}
}
