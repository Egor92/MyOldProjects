using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VNNG2011;

namespace Volga_NN_Gamer_2011
{
	/// <summary>
	/// Логика взаимодействия для ClubPage.xaml
	/// </summary>
	public partial class ClubPage : Page
	{
		#region Конструктор
		public ClubPage()
		{
            InitializeComponent();
            Constructor(3);
		}

		public ClubPage(int code)
        {
            InitializeComponent();
            Constructor(code);
        }

		private void Constructor(int code)
		{
            this.DataContext = DataBase.GetClub(code);
            this.Title = (this.DataContext as Club).FullName;
			DataBase.GetLocationsOfPlayersOfClub((this.DataContext as Club).Manager);
		}
		#endregion

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
			this.PlayersList.ItemsSource = (this.DataContext as Club).Players.OrderBy(x => x.Info.Amplua).ToArray();
        }

        private void LinkToPlayer_Click(object sender, RoutedEventArgs e)
        {
            PlayerPage p = new PlayerPage(((sender as Hyperlink).DataContext as Player).Code);
            NavigationService.Navigate(p);
        }

		private void LinkToCountry_Click(object sender, RoutedEventArgs e)
		{
			CountryPage p = new CountryPage(((sender as Hyperlink).DataContext as Player).Info.FirstNation.Code);
			NavigationService.Navigate(p);
		}


		private void FieldOnSquad_Loaded(object sender, RoutedEventArgs e)
		{
			this.UpGoal_OnSquad.Fill = NetImage;
			this.DownGoal_OnSquad.Fill = NetImage;
		}

		private void FieldOnTactic_Loaded(object sender, RoutedEventArgs e)
		{
			this.UpGoal_OnTactic.Fill = NetImage;
			this.DownGoal_OnTactic.Fill = NetImage;
		}

		private System.Windows.Media.ImageBrush NetImage
		{
			get
			{
				System.Windows.Media.ImageBrush NetImage;
				try
				{
					FileStream FS = new FileStream(@"..\Data\Graphics\Images\Net.png", FileMode.Open, FileAccess.Read, FileShare.Read);
					PngBitmapDecoder PngBD = new PngBitmapDecoder(FS, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
					NetImage = new System.Windows.Media.ImageBrush(PngBD.Frames[0]);
					NetImage.Viewport = new Rect(0, 0, 12, 20);
					NetImage.ViewportUnits = System.Windows.Media.BrushMappingMode.Absolute;
					NetImage.TileMode = System.Windows.Media.TileMode.Tile;
					return NetImage;
				}
				catch 
				{
					return null;				
				}
			}
		}


		private const double RadiusOfFigure = 40;
		private double GetCoordinateOfFigure(double D)
		{
			return D - RadiusOfFigure;
		}


		private void TacticCanvas_Loaded(object sender, RoutedEventArgs e)
		{
			DrawSquadOnTheTacticCanvas();
		}

		private void DrawSquadOnTheTacticCanvas()
		{
            NameOfTactic.Text = (this.DataContext as Club).Manager.CountOfPlayersInAllLines;

            TacticCanvas.Children.Clear();
			Point[] LocationsOfPlayersOfTeam = DataBase.GetLocationsOfPlayersOfClub((this.DataContext as Club).Manager);
			foreach (Point P in LocationsOfPlayersOfTeam)
			{
				Ellipse E = new Ellipse();
				E.Fill = new System.Windows.Media.SolidColorBrush((this.DataContext as Club).HomeKitColor.Background);
				E.Stroke = new System.Windows.Media.SolidColorBrush((this.DataContext as Club).HomeKitColor.Border);
				E.StrokeThickness = 3;
				E.Margin = new Thickness(GetCoordinateOfFigure(P.Y), GetCoordinateOfFigure(P.X), 0.0, 0.0);
				E.Height = 2 * RadiusOfFigure;
				E.Width = 2 * RadiusOfFigure;
				E.MouseMove += new System.Windows.Input.MouseEventHandler(E_MouseMove);
				TacticCanvas.Children.Add(E);
			}
            DefenceAlignment.Content = (this.DataContext as Club).Manager.GetNameofAlignment(1);
            MiddleAlignment.Content  = (this.DataContext as Club).Manager.GetNameofAlignment(2);
            ForwardAlignment.Content = (this.DataContext as Club).Manager.GetNameofAlignment(3);
		}

		void E_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				double X = GetCoordinateOfFigure(e.GetPosition((sender as Ellipse).Parent as Canvas).X);
				if (X < GetCoordinateOfFigure(0) + RadiusOfFigure)
				{ 
					X = GetCoordinateOfFigure(0) + RadiusOfFigure;
				}
				if (X > GetCoordinateOfFigure(680) - RadiusOfFigure) 
				{
					X = GetCoordinateOfFigure(680) - RadiusOfFigure; 
				}

				double Y = GetCoordinateOfFigure(e.GetPosition((sender as Ellipse).Parent as Canvas).Y);
				if (Y < GetCoordinateOfFigure(0) + RadiusOfFigure) 
				{ 
					Y = GetCoordinateOfFigure(0) + RadiusOfFigure;
				}
				if (Y > GetCoordinateOfFigure(1050) - RadiusOfFigure) 
				{ 
					Y = GetCoordinateOfFigure(1050) - RadiusOfFigure; 
				}

				(sender as Ellipse).Margin = new Thickness(X, Y, 0.0, 0.0);
			}
		}


		private void SelectionOfPrevTactic_Click(object sender, RoutedEventArgs e)
		{
			(this.DataContext as Club).Manager.SelectPrevTactic();
			DrawSquadOnTheTacticCanvas();
		}

		private void SelectionOfNextTactic_Click(object sender, RoutedEventArgs e)
		{
			(this.DataContext as Club).Manager.SelectNextTactic();
			DrawSquadOnTheTacticCanvas();
		}

        private void DefenceAlignment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as Club).Manager.Info.AlignmentOfDefence++;
            DrawSquadOnTheTacticCanvas();
        }

        private void MiddleAlignment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as Club).Manager.Info.AlignmentOfMiddlefield++;
            DrawSquadOnTheTacticCanvas();
        }

        private void ForwardAlignment_Click(object sender, System.Windows.RoutedEventArgs  e)
        {
            (this.DataContext as Club).Manager.Info.AlignmentOfForward++;
            DrawSquadOnTheTacticCanvas();
        }


		private void HistoryList_Loaded(object sender, RoutedEventArgs e)
		{
			this.HistoryList.ItemsSource = (this.DataContext as Club).History;
		}


     



	}

	

}
