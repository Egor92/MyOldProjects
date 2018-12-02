using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using VNNG2011;

namespace Volga_NN_Gamer_2011
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            // Получение заголовка страницы
            this.ThisLevel_TextBlock.Text = (this.frame.Content as Page).Title;

            // Ссылка на верхний уровень
            object FrameContent = (this.frame.Content as Page).DataContext;

            this.UpLevel_TextBlock.Inlines.Clear();

            if (FrameContent is Player)
            {
                this.UpLevel_TextBlock.DataContext = (FrameContent as Player).Info.Club;

                this.UpLevel_TextBlock.Inlines.Add(new Run((this.UpLevel_TextBlock.DataContext as Club).FullName));
                this.UpLevel_TextBlock.Click += new RoutedEventHandler(LinkToClub_Click);
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToChampionship_Click);
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToCountry_Click);

				RedefineColorsOfTopLine((FrameContent as Player).Info.Club.ColorsOfTopLine);
                ShowLogotype(@"..\Data\Graphics\Logos\Clubs\", (FrameContent as Player).Info.Club.Code);
            }

            if (FrameContent is Club)
            {
                this.UpLevel_TextBlock.DataContext = (FrameContent as Club).League;

                this.UpLevel_TextBlock.Inlines.Add(new Run((this.UpLevel_TextBlock.DataContext as Championship).Name));
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToClub_Click);
                this.UpLevel_TextBlock.Click += new RoutedEventHandler(LinkToChampionship_Click);
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToCountry_Click);

				RedefineColorsOfTopLine((FrameContent as Club).ColorsOfTopLine);
                ShowLogotype(@"..\Data\Graphics\Logos\Clubs\", (FrameContent as Club).Code);
            }

            if (FrameContent is Championship)
            {
                this.UpLevel_TextBlock.DataContext = (FrameContent as Championship).Country;

                this.UpLevel_TextBlock.Inlines.Add(new Run((this.UpLevel_TextBlock.DataContext as Country).Name));
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToClub_Click);
                this.UpLevel_TextBlock.Click -= new RoutedEventHandler(LinkToChampionship_Click);
                this.UpLevel_TextBlock.Click += new RoutedEventHandler(LinkToCountry_Click);

				RedefineColorsOfTopLine((FrameContent as Championship).Country.ColorsOfTopLine);
                ShowLogotype(@"..\Data\Graphics\Logos\Championships\", (FrameContent as Championship).Code);
            }

            if (FrameContent is Country)
            {
				RedefineColorsOfTopLine((FrameContent as Country).ColorsOfTopLine);
                ShowLogotype(@"..\Data\Graphics\Logos\Nations\", (FrameContent as Country).Code);
            }
        }

        //Перекраска верхней полосы
		private void RedefineColorsOfTopLine(VNNG2011.KitColor color)
		{
			this.TopLine.Background = color.BackColorOfTopLine;
			this.ThisLevel_TextBlock.Foreground = color.ForeColorOfTopLine;
			this.UpLevel_TextBlock.Foreground = color.ForeColorOfTopLine;
		}

		//Отобразить герб
		private void ShowLogotype(string Directory, int Code)
		{
			try
			{
				FileStream FS = new FileStream(Directory + Code.ToString() + ".png", FileMode.Open, FileAccess.Read, FileShare.Read);
				PngBitmapDecoder PngBD = new PngBitmapDecoder(FS, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
				this.Logotype.Source = PngBD.Frames[0];
			}
			catch
			{
				try
				{
					FileStream FS = new FileStream(@"..\Data\Graphics\Images\default.png", FileMode.Open, FileAccess.Read, FileShare.Read);
					PngBitmapDecoder PngBD = new PngBitmapDecoder(FS, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
					this.Logotype.Source = PngBD.Frames[0];
				}
				catch { }
			}
		}


		void LinkToClub_Click(object sender, RoutedEventArgs e)
		{
			ClubPage p = new ClubPage(((sender as Hyperlink).DataContext as Club).Code);
			frame.Navigate(p);
		}

		void LinkToChampionship_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink h = (sender as Hyperlink);
			object o = h.DataContext;
			Championship c = o as Championship;
			int i = c.Code;
			ChampionshipPage cp = new ChampionshipPage(i);
			ChampionshipPage p = new ChampionshipPage(((sender as Hyperlink).DataContext as Championship).Code);
			frame.Navigate(p);
		}

		void LinkToCountry_Click(object sender, RoutedEventArgs e)
		{
			CountryPage p = new CountryPage(((sender as Hyperlink).DataContext as Country).Code);
			frame.Navigate(p);
		}
	}
}
