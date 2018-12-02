using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace VNNG2011
{
    //public static class GameData
    //{
    //    int Code;G:\Егор\Volga NN Gamer 2011\Volga NN Gamer 2011 v2\ClassLibrary\ClassData.cs
    //    DateTime Date;
    //    int Difficulty;

    //    int PersonCode;
    //    RingBuffer TacticCode;
    //    int CoachOfGKCode;
    //    int CoachOfDefenceCode;
    //    int CoachOfMiddlefieldCode;
    //    int CoachOfAttackCode;
    //    int CoachOfFitnessCode;
    //    int ScoutCode;
    //    int PhysiotherapistCode;

    //}

    public class Player
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

        private PlayerInfo info;
        private PhysicalAttr physicalAttr;
        private MentalAttr mentalAttr;
        private TechAttr techAttr;
        private GKAttr gkAttr;

        public PlayerInfo Info { get { if (info == null) { info = DataBase.GetPlayerInfo(Code); } return info; } }
        public PhysicalAttr PhysicalAttr { get { if (physicalAttr == null) { physicalAttr = DataBase.GetPhysAttr(Code); } return physicalAttr; } }
        public MentalAttr MentalAttr { get { if (mentalAttr == null) { mentalAttr = DataBase.GetMentalAttr(Code); } return mentalAttr; } }
        public TechAttr TechAttr { get { if (techAttr == null) { techAttr = DataBase.GetTechAttr(Code); } return techAttr; } }
        public GKAttr GKAttr { get { if (gkAttr == null) { gkAttr = DataBase.GetGKAttr(Code); } return gkAttr; } }
        public Injury[] Injuries { get { return  DataBase.GetInjuries(Code); } }
        public PlayedMatches[] PlayedMathes { get { return DataBase.GetPlayedMatches(Code); } }

        public void AddInjury(int Type, DateTime RecoverDate) { DataBase.AddInjury(Code, Type, RecoverDate); }

        public Player(int Code) { this.Code = Code; }            
    }

    public class Referee
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

        public RefereeInfo Info { get { return DataBase.GetRefereeInfo(Code); } }

        public Referee(int Code) { this.code = Code; }
    }

	//TODO
    public class Manager
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

		private ManagerInfo info;
        public ManagerInfo Info 
		{
			get 
			{
				if (this.info == null)
                {
                    this.info = DataBase.GetManagerInfo(this);
                    this.info.AlignmentOfDefence.Size = DataBase.GetCountOfTactics(this, 1);
                    this.info.AlignmentOfMiddlefield.Size = DataBase.GetCountOfTactics(this, 2);
                    this.info.AlignmentOfForward.Size = DataBase.GetCountOfTactics(this, 3);
                }
				return this.info; 
			}
		}

		public System.Windows.Point[] LocationsOfPlayersOfClub
		{
			get
			{
				return DataBase.GetLocationsOfPlayersOfClub(this);
			}
		}
        public string CountOfPlayersInAllLines {
            get {
                int[] c = DataBase.GetCountOfPlayersInAllLines(Info.Tactic);
                return c[0] + "-" + c[1] + "-" + c[2];
            } }
		//TODO
		public void SelectNextTactic()
		{
			this.Info.Tactic++;
            
			//TODO: Вызывать конструкторы!
            this.Info.AlignmentOfDefence = new RingBuffer(DataBase.GetCountOfTactics(this, 1));
            this.Info.AlignmentOfMiddlefield = new RingBuffer(DataBase.GetCountOfTactics(this, 2));
            this.Info.AlignmentOfForward = new RingBuffer(DataBase.GetCountOfTactics(this, 3));
		}
		//TODO
		public void SelectPrevTactic()
		{
			this.Info.Tactic--;

			//TODO: Вызывать конструкторы!
			this.Info.AlignmentOfDefence.Value = 1;
			this.Info.AlignmentOfMiddlefield.Value = 1;
			this.Info.AlignmentOfForward.Value = 1;
		}


        public string GetNameofAlignment(int line)
        {
            return DataBase.GetNameOfAlignment(DataBase.Opredelitel(this, line, true));
        }

        public Manager(int Code) { this.code = Code; }
    }

    public class Club
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

		private string fullName;
		public string FullName { get { return fullName; } set { fullName = value; } }

		private string shortName;
		public string ShortName	{ get { return shortName; } set { shortName = value; } }

		private int reputation;
        public int Reputation { get { return reputation; } set { reputation = value; } }

		private int budget;
        public int Budget { get { return budget; } set { budget = value; } }
        
        private int city;
        private int stadium;

        private int managerCode;
        private Manager manager;
        public Manager Manager {
			get 
			{
				if (managerCode != 0) 
				{ 
					if (manager == null) 
						manager = DataBase.GetManager(managerCode);
					return manager; 
				}
				else
					return null;
			}
		}

        private KitColor[] colors;
        public KitColor[] Colors { get { if (colors == null) colors = DataBase.GetColorsOfClub(code); return colors; } }
        public KitColor ColorsOfTopLine { get { try { return Colors[0]; } catch { return KitColor.Default; } } }
        public KitColor[] KitColors 
		{
			get 
			{
				KitColor[] Result = this.Colors.SkipWhile(x => x.Number <= 0).ToArray();
				if (Result.Length == 0)
				{
					Result = new KitColor[1] { KitColor.Default };
				}
				return Result;
			} 
		}
        public KitColor HomeKitColor 
		{
			get
			{
				return this.KitColors [ this.KitColors.Min(x => x.Number) - 1 ];
			}
		}

        public HistoryOfClub[] History { get { return DataBase.GetHistoryOfClub(code).OrderBy(x => x.Season).ToArray(); } }
        public Stadium Stadium { get { return DataBase.GetStadium(stadium); } }
        public City City { get { return DataBase.GetCity(city); } }
        public Player[] Players { get { return DataBase.GetPlayers(code); } }
        public MatchOfClub[] Matches { get { return DataBase.GetMatchesOfClubs("Дом = " + code + " OR Гости = " + code); } }
        public Championship[] Champs { get { return DataBase.GetChampionships(code); } }
        public Championship League { get { return DataBase.GetChampionships(code, TypeOfChamp.League)[0]; } }

        public Club(int Code, string FullName, string ShortName, int City, int Budget, int Reputation, int Stadium, int Manager)
        {
            this.managerCode = Manager;
            this.code = Code;
            this.fullName = FullName;
            this.shortName = ShortName;
            this.reputation = Reputation;
            this.city = City;
            this.stadium = Stadium;
            this.budget = Budget;
        }
    }
    
    public class City
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

		private string name;
		public string Name { get { return name; } set { name = value; } }

		private int reputation;
		public int Reputation { get { return reputation; } set { reputation = value; } }

        private int CountryCode;

        public Country Country { get { return DataBase.GetCountry(CountryCode); } }
        public Club[] Clubs { get { return DataBase.GetClubs("Город = " + Code); } }
        public Stadium[] Stadiums { get { return DataBase.GetStadiums("Город = " + Code); } }
       
                
        public City(int Code, string Name, int Reputation, int Country)
        {
            this.code = Code;
            this.name = Name;
            this.reputation = Reputation;
            this.CountryCode = Country;
            
        }
    }

    public class Championship
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

		private string name;
		public string Name { get { return name; } set { name = value; } }

		private int type;
		public int Type { get { return type; } set { type = value; } }

		private int countOfClubs;
		public int CountOfClubs { get { return countOfClubs; } set { countOfClubs = value; } }

		private int reputation;
		public int Reputation { get { return reputation; } set { reputation = value; } }

		private bool isActive;
		public bool IsActive { get { return isActive; } set { isActive = value; } }

        private int CountryCode;

        public Club[] Clubs { get { return DataBase.GetClubsOfChamp(Code); } }
        public Country Country { get { return DataBase.GetCountry(CountryCode); } }

        public HistoryOfChampionship[] History { get { return DataBase.GetHistoryOfChampionship(Code); } }
		public KitColor ColorsOfTopLine { get { return Country.ColorsOfTopLine; } }

        public Championship(int Code, string Name, int Country, int Type, int CountOfClubs, int Reputation, bool IsActive)
        {
            this.code = Code;
            this.name = Name; 
            this.CountryCode = Country;
            this.type = Type;
            this.countOfClubs = CountOfClubs;
            this.reputation = Reputation;
            this.isActive = IsActive;
        }
    }

    public class Stadium
    {
		private int code;
		public int Code { get { return code; } set { code = value; } }

		private string name;
		public string Name { get { return name; } set { name = value; } }

		private int capacity;
		public int Capacity { get { return capacity; } set { capacity = value; } }


        private int CityCode;
	
	
        public City City { get { return DataBase.GetCity(CityCode); } }
        public Club[] Clubs { get { return DataBase.GetClubs("Стадион = " + Code); } }

        public Stadium(int Code, string Name, int City, int Capacity)
        {
            this.code = Code;
            this.name = Name;
            this.CityCode = City;
            this.capacity = Capacity;
        }
    }

    #region Матчи
    public class Match
    {
        protected int code;
        public int Code { get { return code; } set { code = value; } }

        protected DateTime date;
        public DateTime Date { get { return date; } set { date = value; } }

        protected int round;
        public int Round { get { return round; } set { round = value; } }

        protected int goalHome;
        public int GoalHome { get { return goalHome; } set { goalHome = value; } }

        protected int goalGuest;
        public int GoalGuest { get { return goalGuest; } set { goalGuest = value; } }

        protected int penaltyHome;
        public int PenaltyHome { get { return penaltyHome; } set { penaltyHome = value; } }

        protected int penaltyGuest;
        public int PenaltyGuest { get { return penaltyGuest; } set { penaltyGuest = value; } }


        protected int ChampionshipCode;
        protected int HomeCode;
        protected int GuestCode;

        public Championship Championship { get { return DataBase.GetChampionship(ChampionshipCode); } }

        public Match(int Code, DateTime Date, int Championship, int Round, int Home, int Guest, int GoalHome, int GoalGuest, int PenaltyHome, int PenaltyGuest)
        {
            this.code = Code;
            this.date = Date;
            this.ChampionshipCode = Championship;
            this.round = Round;
            this.HomeCode = Home;
            this.GuestCode = Guest;
            this.goalHome = GoalHome;
            this.goalGuest = GoalGuest;
            this.penaltyHome = PenaltyHome;
            this.penaltyGuest = PenaltyGuest;
        }
    }

    public class MatchOfClub : Match
    {

        public Club Home { get { return DataBase.GetClub(HomeCode); } }
        public Club Guest { get { return DataBase.GetClub(GuestCode); } }

        public MatchOfClub(int Code, DateTime Date, int Championship, int Round, int Home, int Guest, int GoalHome, int GoalGuest, int PenaltyHome, int PenaltyGuest)
            : base(Code, Date, Championship, Round, Home, Guest, GoalHome, GoalGuest, PenaltyHome, PenaltyGuest) { }
        
    }

    public class MatchOfCountry : Match
    {

        public Country Home { get { return DataBase.GetCountry(HomeCode); } }
        public Country Guest { get { return DataBase.GetCountry(GuestCode); } }

        public MatchOfCountry(int Code, DateTime Date, int Championship, int Round, int Home, int Guest, int GoalHome, int GoalGuest, int PenaltyHome, int PenaltyGuest)
            : base(Code, Date, Championship, Round, Home, Guest, GoalHome, GoalGuest, PenaltyHome, PenaltyGuest) {}
    }
    #endregion

    #region Информация о людях

    public class Info
    {
		private string name;
		public string Name { get { return name; } set { name = value; } }

		private string surname;
		public string Surname { get { return surname; } set { surname = value; } }

		public string N_Surname { get { return (name == string.Empty ? string.Empty : name.First() + ". ") + surname; } }
		public string Name_Surname { get { return (name == string.Empty ? string.Empty : name + " ") + surname; } }

		private int reputation;
		public int Reputation { get { return reputation; } set { reputation = value; } }

		private DateTime dateOfBirthday;
		public string DateOfBirthday { get { return dateOfBirthday.ToShortDateString(); } }
		public string Age
		{ 
			get 
			{
				int Result = DateTime.Today.Year - dateOfBirthday.Year;
				if (dateOfBirthday.DayOfYear < DateTime.Today.DayOfYear)
				{
					Result--;
				}
				return GetNormalizedAge(Result);
			} 
		}
		private string GetNormalizedAge(int Age)
		{
			if (Age % 10 == 1)
			{
				return Age.ToString() + " год";
			}
			else if (Age % 10 >= 2 && Age % 10 <= 4)
			{
				return Age.ToString() + " года";
			}
			else
			{
				return Age.ToString() + " лет";
			}
		}

		private int firstNation;
		private int secondNation;
        protected int Code;
        
        public Country FirstNation { get { return DataBase.GetCountry(firstNation); } }
        public Country SecondNation { get { return DataBase.GetCountry(secondNation); } }

        public Info(int Code, String Name, String Surname, int Reputation, string BirthDay, int FirstNation, int SecondNation)
        {
            this.Code = Code;
            this.name = Name.Trim();
            this.surname = Surname.Trim();
            this.reputation = Reputation;
            
            if (BirthDay.Length != 0)
            {
                dateOfBirthday = DateTime.Parse(BirthDay);
            }
            this.firstNation = FirstNation;
            this.secondNation = SecondNation;
        }
    }

    public class PlayerInfo : Info
    {
		private int number;
		public int Number { get { return number; } set { number = value; } }

		private int amplua;
		public int Amplua { get { return amplua; } set { amplua = value; } }

		private int height;
		public int Height { get { return height; } set { height = value; } }

		private int weight;
		public int Weight { get { return weight; } set { weight = value; } }

		private int attrPriority;
		public int AttrPriority { get { return attrPriority; } set { attrPriority = value; } }

		public KitColor[] Colors { get { return Club.Colors; } }

        private int ClubCode;

        public Club Club { get { return DataBase.GetClub(ClubCode); } set { } }

        private int LeaseClubCode;
        
        public Club LeaseClub { get { if (LeaseClubCode != 0) { return DataBase.GetClub(LeaseClubCode); } else { return null; } } }
        public PlayedMatches[] PlayedMathes { get { return DataBase.GetPlayedMatches(Code); } }
        
        public PlayerInfo(int Code, string Name, String Surname, int Reputation, int Club, string BirthDay, int FirstNation, int SecondNation, int Number, int Amplua, int Height, int Weight,int LeaseClub, int AttrPriority)
            : base(Code, Name, Surname, Reputation, BirthDay, FirstNation, SecondNation)
        {
            this.height = Height;
            this.weight = Weight;
            this.amplua = Amplua;
            this.number = Number;
            this.attrPriority = AttrPriority;
            this.LeaseClubCode = LeaseClub;
            this.ClubCode = Club;
        }

    }

    public class RefereeInfo : Info
    {
        private int CityCode;

        public City City { get { return DataBase.GetCity(CityCode); } }
        
        public RefereeInfo(int Code, string Name, String Surname, int Reputation, string BirthDay, int FirstNation, int SecondNation, int City)
            : base(Code, Name, Surname, Reputation, BirthDay, FirstNation, SecondNation)
        {
            this.CityCode = City;
        }
    }

    public class ManagerInfo : Info
    {
		private RingBuffer tactic;
		public RingBuffer Tactic { get { return tactic; } set { tactic = value; } }

		private RingBuffer alignmentOfDefence;
		public RingBuffer AlignmentOfDefence { get { return alignmentOfDefence; } set { alignmentOfDefence = value; } }

		private RingBuffer alignmentOfMiddlefield;
        public RingBuffer AlignmentOfMiddlefield { get { return alignmentOfMiddlefield; } set { alignmentOfMiddlefield = value; } }

		private RingBuffer alignmentOfForward;
		public RingBuffer AlignmentOfForward { get { return alignmentOfForward; } set { alignmentOfForward = value; } }

       
        public Club Club { get { try { return DataBase.GetClubs("Менеджер = " + Code)[0]; } catch { return null; } } }

        public ManagerInfo(int Code, string Name, String Surname, int Reputation, string BirthDay, int FirstNation, int SecondNation, RingBuffer Tactic, RingBuffer AlignmentOfDefence, RingBuffer AlignmentOfMiddlefield, RingBuffer AlignmentOfForward)
            : base(Code, Name, Surname, Reputation, BirthDay, FirstNation, SecondNation)
        {
            this.tactic = Tactic;
            this.alignmentOfForward = AlignmentOfForward;
            this.alignmentOfDefence = AlignmentOfDefence;
            this.alignmentOfMiddlefield = AlignmentOfMiddlefield;
        }
    }

    #endregion

    #region Аттрибуты
    public class TechAttr
    {
        internal int Code;
        public int Dribbling;   //Дриблинг
        public int Finishing;   //Завершение атаки
        public int HeadGame;    //Игра головой
        public int FreeKick;    //Штрафные удары
        public int LobKick;     //Навесы
        public int Taking;      //Отбор
        public int Pass;        //Пас
        public int Technique;   //Техника
        public int LongKick;    //Дальние удары

        public TechAttr(int Code, int Dribbling, int Finishing, int HeadGame, int FreeKick, int LobKick, int Taking, int Pass, int Technique, int LongKick)
        {
            this.Code = Code;
            this.Dribbling = Dribbling;
            this.Finishing = Finishing;
            this.HeadGame = HeadGame;
            this.FreeKick = FreeKick;
            this.LobKick = LobKick;
            this.Taking = Taking;
            this.Pass = Pass;
            this.Technique = Technique;
            this.LongKick = LongKick;
        }
    }

    public class MentalAttr
    {
        internal int Code;
        public int PositionSelect;  //Выбор позиции
        public int TeamGame;        //Командная игра
        public int Concentration;   //Концентрация
        public int ProblemSolving;  //Принятие решений
        public int Creation;        //Созидание
        public int SelfControl;     //Самообладание
        public int Tactic;          //Тактика
        public int Marking;         //Опека
        public int Moral;           //Мораль

        public MentalAttr(int Code, int PositionSelect, int TeamGame, int Concentration, int ProblemSolving, int Creation, int SelfControl, int Tactic, int Marking, int Moral)
        {
            this.Code = Code;
            this.PositionSelect = PositionSelect;
            this.TeamGame = TeamGame;
            this.Concentration = Concentration;
            this.ProblemSolving = ProblemSolving;
            this.Creation = Creation;
            this.SelfControl = SelfControl;
            this.Tactic = Tactic;
            this.Marking = Marking;
            this.Moral = Moral;
        }
    }

    public class PhysicalAttr
    {
        internal int Code;
        public int Stamina;         //Выносливость
        public int Dexterity;       //Ловкость
        public int Jumping;         //Прыгучесть
        public int Power;           //Сила
        public int Speed;           //Скорость
        public int Acceleration;    //Ускорение
        public int Balance;         //Баланс
        public int Reaction;        //Реакция
        public int Fatigue;         //Усталость

        public void Save()
        {
            db.AttributesRow p = DataBase.TableOfAttributes.FindByКод(this.Code);

            p["Выносливость"] = this.Stamina;
            p["Ловкость"] = this.Dexterity;
            p["Прыгучесть"] = this.Jumping;
            p["Сила"] = this.Power;
            p["Скорость"] = this.Speed;
            p["Ускорение"] = this.Acceleration;
            p["Баланс"] = this.Balance;
            p["Реакция"] = this.Reaction;
            p["Усталость"] = this.Fatigue;
            p.AcceptChanges();
            DataBase.TableOfAttributes.AcceptChanges();
            //DataBase.UpdateAttributes(p);
        }

        ~PhysicalAttr() { Save(); }

        public PhysicalAttr(int Code, int Stamina, int Dexterity, int Jumping, int Power, int Speed, int Acceleration, int Balance, int Reaction, int Fatigue)
        {
            this.Code = Code;
            this.Stamina = Stamina;
            this.Dexterity = Dexterity;
            this.Jumping = Jumping;
            this.Power = Power;
            this.Speed = Speed;
            this.Acceleration = Acceleration;
            this.Balance = Balance;
            this.Reaction = Reaction;
            this.Fatigue = Fatigue;
        }
    }

    public class GKAttr
    {
        internal int Code;
        public int Throwing;          //Ввод мяча
        public int AirGame;           //Игра в воздухе
        public int HandGame;          //Игра руками
        public int OneToOne;          //Один на один
        public int DefenceOrganize;   //Организация обороны
        public int Reflex;            //Рефлекс

        public GKAttr(int Code, int Throwing, int AirGame, int HandGame, int OneToOne, int DefenceOrganize, int Reflex)
        {
            this.Code = Code;
            this.Throwing = Throwing;
            this.AirGame =  AirGame;
            this.HandGame = HandGame;
            this.OneToOne =  OneToOne;
            this.DefenceOrganize =  DefenceOrganize;
            this.Reflex = Reflex;
        }
    }
    #endregion

    public class PlayedMatches
    {
        public int Code;
        // TODO: Переименовать!
        public bool IsHome;
        public int Goals;
        public int GolPeredachi;
        public int YellowCards;
        public int RedCards;
        public int Minutes;
        // TODO: ?
        public double Mark;
        
        private int PlayerCode;
        private int MatchCode;

        public Player Player { get { return DataBase.GetPlayer(PlayerCode); } }
        public MatchOfClub Match { get { return DataBase.GetMatchOfClub(MatchCode); } }
        
        public PlayedMatches(int Code, int Player, int Match, bool Side, int Goals, int GolPeredachi, int YellowCards, int RedCards, int Minutes, double Mark)
        {
            this.Code = Code;
            this.PlayerCode = Player;
            this.MatchCode = Match;
            this.IsHome = Side;
            this.Goals = Goals;
            this.GolPeredachi = GolPeredachi;
            this.YellowCards = YellowCards;
            this.RedCards = RedCards;
            this.Minutes = Minutes;
            this.Mark = Mark;
        }
    
}

    public class Injury
    {
        public int Code;
        public int PlayerCode;
        public DateTime RecoverDate;

        private int TypeCode;

        public TypeOfInjury Type { get { return DataBase.GetTypeOfInjury(TypeCode); } }

        public Injury(int Code, int PlayerCode, int Type, DateTime RecoverDate)
        {
            this.Code = Code;
            this.PlayerCode = PlayerCode;
            this.TypeCode = Type;
            this.RecoverDate = RecoverDate;
        }
    }

    public class KitColor
    {
        public int Number;

        Color background;
        public System.Windows.Media.Color Background 
		{ 
			get 
			{
				byte A = this.background.A;
				byte R = System.Convert.ToByte(this.background.R);
				byte G = System.Convert.ToByte(this.background.G);
				byte B = System.Convert.ToByte(this.background.B);
				return System.Windows.Media.Color.FromArgb(A, R, G, B);
			}
		}

        Color foreground;
		public System.Windows.Media.Color Foreground
		{
			get
			{
				byte A = this.foreground.A;
				byte R = System.Convert.ToByte(this.foreground.R);
				byte G = System.Convert.ToByte(this.foreground.G);
				byte B = System.Convert.ToByte(this.foreground.B);
				return System.Windows.Media.Color.FromArgb(A, R, G, B);
			}
		}

		Color border;
		public System.Windows.Media.Color Border
		{
			get
			{
				byte A = this.border.A;
				byte R = System.Convert.ToByte(this.border.R);
				byte G = System.Convert.ToByte(this.border.G);
				byte B = System.Convert.ToByte(this.border.B);
				return System.Windows.Media.Color.FromArgb(A, R, G, B);
			}
		}

		public System.Windows.Media.LinearGradientBrush BackColorOfTopLine
		{
			get
			{
				byte A = this.background.A;
				byte R = System.Convert.ToByte(this.background.R);
				byte G = System.Convert.ToByte(this.background.G);
				byte B = System.Convert.ToByte(this.background.B);

				System.Windows.Media.Color StartColor = System.Windows.Media.Color.Multiply(
					System.Windows.Media.Color.FromRgb(
						this.background.R,
						this.background.G,
						this.background.B),
						1.5f);
				StartColor.A = 255;

				System.Windows.Media.Color EndColor   =  System.Windows.Media.Color.Multiply(
					System.Windows.Media.Color.FromRgb(
						this.background.R,
						this.background.G,
						this.background.B),
						0.5f);
				EndColor.A = 255;

				System.Windows.Media.LinearGradientBrush GradientBrush =
					new System.Windows.Media.LinearGradientBrush(StartColor,
																EndColor,
																new System.Windows.Point(0.5, 0),
																new System.Windows.Point(0.5, 1));
				GradientBrush.MappingMode = System.Windows.Media.BrushMappingMode.RelativeToBoundingBox;

				return GradientBrush;
			}
		}

		public System.Windows.Media.SolidColorBrush ForeColorOfTopLine
		{
			get
			{
				byte A = this.foreground.A;
				byte R = this.foreground.R;
				byte G = this.foreground.G;
				byte B = this.foreground.B;
				return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(A, R, G, B));
			}
		}

		public static KitColor Default { get { return new KitColor(Color.Black, Color.White, Color.Black, 1); } }

        public KitColor() {}

        public KitColor(Color Foreground, Color Background, Color Border, int Number)
        {
			this.foreground = Foreground;
			this.background = Background;
			this.border = Border;
			this.Number = Number;
        }

        public KitColor(DataRow s)
        {
            this.foreground = DataBase.GetColor((int?)s["Шрифт"]);
            this.background = DataBase.GetColor((int?)s["Фон"]);
            this.border = DataBase.GetColor((int?)s["Край"]);
            this.Number = (int)s["Номер"];
        }
    }

    public enum Amplua
    {
        _FORWARD=-3,
        _MIDDLEFIELD,
        _DEFENDER,
        _NONE,
        GK,    //GK
        CD,   //CD
        FD,     //
        OM,
        CM,
        FM,
        AM,
        FR
    }

    public enum TypeOfChamp
    {League=1, Cup, Supercup, Friendly}

    public class TypeOfInjury
    {
        public int Code;
        public string Name;
        public int MinLenght;
        public int MaxLenght;
        public bool OnlyInGame;

        public TypeOfInjury(int Code, string Name, int MinLenght, int MaxLenght, bool OnlyInGame)
        {
            this.Code = Code;
            this.Name = Name;
            this.MinLenght = MinLenght;
            this.MaxLenght = MaxLenght;
        }
    }

    public class Country
    {
        private int code;
        public int Code { get { return code; } set { code = value; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int reputation;
        public int Reputation { get { return reputation; } set { reputation = value; } }

        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        private int managerCode;
        private Manager manager;
        public Manager Manager { get { if (managerCode != 0) { if (manager == null) manager = DataBase.GetManager(managerCode); return manager; } else return null; } }

        public Championship[] Championships { get { return DataBase.GetChampionships("Страна = " + code); } }

        public KitColor[] Colors { get { return DataBase.GetColorsOfCountry(Code); } }
        public KitColor ColorsOfTopLine 
		{
			get 
			{ 
				try 
				{ 
					return Colors[0]; 
				} 
				catch 
				{ 
					return KitColor.Default; 
				} 
			} 
		}

		public City[] Cities { get { return DataBase.GetCities("Страна = " + code); } }
        public MatchOfCountry[] Matches { get { return DataBase.GetMatchesOfCountry("Дом = " + Code + " OR Гости = " + Code); } }

        public Country(int Code, string Name, int Reputation, bool IsActive, int Manager)
        {
            this.managerCode = Manager;
            this.code = Code;
            this.name = Name;
            this.reputation = Reputation;
            this.isActive = IsActive;
        }

    }

    public class Transfer
    {
        public int Code;
        public DateTime Date;
        public int Amount;

        private int PlayerCode;
        private int FromCode;
        private int ToCode;

        public Player Player { get { return DataBase.GetPlayer(PlayerCode); } }
        public Club From { get { return DataBase.GetClub(FromCode); } }
        public Club To { get { return DataBase.GetClub(ToCode); } }

        public Transfer(int Code, int Player, int FromClub, int ToClub, string Date, int Amount)
        {
            this.Code = Code;
            this.PlayerCode = Player;
            this.FromCode = FromClub;
            this.ToCode = ToClub;
            this.Date = DateTime.Parse(Date);
            this.Amount = Amount;
        }
    }

	public enum FootballAmplua
	{
		Defence = 1,
		Middlefield,
		Attack
	}

	public class RingBuffer
	{
		private int value;
		public int Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		private int size;
		public int Size
		{
		    get { return size; }
            set { size = value; }
		}

		public RingBuffer(int Size, int InitialValue = 1)
		{
			this.value = InitialValue;
			this.size = Size;
		}

		public static RingBuffer operator ++ (RingBuffer buffer)
		{
			if (buffer.value == buffer.size)
			{
				buffer.value = 1;
			}
			else
			{
				buffer.value += 1;
			}
			return buffer;
		}

		public static RingBuffer operator -- (RingBuffer buffer)
		{
			if (buffer.value == 1)
			{
				buffer.value = buffer.size;
			}
			else
			{
				buffer.value -= 1;
			}
			return buffer;
		}
	}

    public class HistoryOfClub
    {
        private int code;
        public int Code { get { return code; } set { code = value; } }

        private int clubCode;
        public Club Club { get { return DataBase.GetClub(clubCode); } }

        private string season;
        public string Season { get { return season; } set { season = value; } }

        private int championship;
		public Championship Championship
		{
			get 
			{
				return DataBase.GetChampionship(championship);
			} 
		}
		public String ChampionshipName
		{
			get
			{
				if (etc == string.Empty)
				{
					return Championship.Name;
				}
				else
				{
					return etc;
				}
			}
		}

        private int wins;
        public int Wins { get { return wins; } set { wins = value; } }

        private int drawns;
        public int Drawns { get { return drawns; } set { drawns = value; } }

        private int loses;
        public int Loses { get { return loses; } set { loses = value; } }

        private int goals;
        public int Goals { get { return goals; } set { goals = value; } }

        private int passedBalls;
        public int PassedBalls { get { return passedBalls; } set { passedBalls = value; } }

        private int position;
        public int Position { get { return position; } set { position = value; } }

        private string etc;


        public HistoryOfClub(int Code, int ClubCode, string Season, int Championship, int WinnedGames, int DrawnGames, int LoseGames, int Goals, int PassedBalls, int Position, string etc)
        {
            this.code = Code;
            this.clubCode = ClubCode;
            this.season = Season;
            this.championship = Championship;
            this.wins = WinnedGames;
            this.drawns = DrawnGames;
            this.loses = LoseGames;
            this.goals = Goals;
            this.passedBalls = PassedBalls;
            this.position = Position;
            this.etc = etc;
        }
     }

    public class HistoryOfChampionship
    {
        int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        string season;
        public string Season
        {
            get { return season; }
            set { season = value; }
        }

        int championship;
        public int Championship
        {
            get { return championship; }
            set { championship = value; }
        }

        int firstPosition;
        public int FirstPosition
        {
            get { return firstPosition; }
            set { firstPosition = value; }
        }

        int secondPosition;
        public int SecondPosition
        {
            get { return secondPosition; }
            set { secondPosition = value; }
        }

        int thirdPosition;
        public int ThirdPosition
        {
            get { return thirdPosition; }
            set { thirdPosition = value; }
        }

        string goalScorers;
        string etc;

        public HistoryOfChampionship(int code, string season, int championship, int firstPosition, int secondPosition, int thirdPosition, string goalScorers, string etc)
        {
            this.code = code;
            this.season = season;
            this.championship = championship;
            this.firstPosition = firstPosition;
            this.secondPosition = secondPosition;
            this.thirdPosition = thirdPosition;
            this.goalScorers = goalScorers;
            this.etc = etc;
        }
    }
}
