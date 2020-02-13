using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary.DBClass;

namespace VNNG2013.Resources.Controls
{
	/// <summary>
	/// Логика взаимодействия для PlayerControl.xaml
	/// </summary>
	public partial class PlayerControl : UserControl
	{
		public PlayerControl()
		{
			InitializeComponent();
		}

		public static double Radius
		{
			get { return 40.0; }
		}

		public static double Diametr
		{
			get { return 2 * PlayerControl.Radius; }
		}

		public int Number
		{
			get { return (int)this.GetValue(PlayerControl.NumberProperty); }
			set { this.SetValue(PlayerControl.NumberProperty, value); }
		}
		public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(PlayerControl));

		/*public double X
		{
			get { return (double)this.GetValue(PlayerControl.XProperty); }
			set { this.SetValue(PlayerControl.XProperty, value); }
		}
		public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(PlayerControl));

		public double Y
		{
			get { return (double)this.GetValue(PlayerControl.YProperty); }
			set { this.SetValue(PlayerControl.YProperty, value); }
		}
		public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(PlayerControl));
		*/
		private Club CurrentClub
		{
			get { return this.DataContext as Club; }
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.CurrentClub != null)
			{
				this.SetLocations();
				this.SetKits();
			}
		}

		private void SetLocations()
		{
			Point[] locations = this.CurrentClub.CurrentTactic.PositionInfoes.Select(x => x.Location).ToArray();
			//Если полевой игрок или вратарь
			if (this.Number <= 10)
			{
				Canvas.SetLeft(this, locations[this.Number].X - PlayerControl.Radius);
				Canvas.SetTop(this, locations[this.Number].Y - PlayerControl.Radius);
			}
			//Если игрок замены
			//TODO
		}

		private void SetKits()
		{
			Kit kit = null;
			//Если вратарь
			if (this.Number == 0)
				kit = this.CurrentClub.GKKit;
			//Если полевой игрок или игрок замены
			else
				kit = this.CurrentClub.HomeKit;
			this.NumberTextBlock.Foreground = new SolidColorBrush(kit.Foreground);
			this.Round.Fill = new SolidColorBrush(kit.Background);
			this.Round.Stroke = new SolidColorBrush(kit.Border);
		}


	}
}
