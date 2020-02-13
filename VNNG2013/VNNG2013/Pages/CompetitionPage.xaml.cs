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
using ClassLibrary;
using ClassLibrary.DBClass;

namespace VNNG2013.Pages
{
	/// <summary>
	/// Логика взаимодействия для CompetitionPage.xaml
	/// </summary>
	public partial class CompetitionPage : BasePage
	{
		public CompetitionPage(Competition competition) : base(competition)
		{
			InitializeComponent();
		}

		private void ToClubClick(object sender, RoutedEventArgs e)
		{
			this.Navigate((sender as Hyperlink).DataContext as DBObject);
		}


	}
}
