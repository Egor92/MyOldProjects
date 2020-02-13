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
using ClassLibrary;
using ClassLibrary.Resources.Tactics;
using VNNG2013.Resources.Controls;
using VNNG2013.Windows;

namespace VNNG2013.Pages
{
	/// <summary>
	/// Логика взаимодействия для ClubPage.xaml
	/// </summary>
	public partial class ClubPage : BasePage
	{
		public ClubPage(Club club) : base(club)
		{
			InitializeComponent();
		}

		private void ToPersonClick(object sender, RoutedEventArgs e)
		{
			this.Navigate((sender as Hyperlink).DataContext as DBObject);
		}

		private void ToNationClick(object sender, RoutedEventArgs e)
		{
			this.Navigate(((sender as Hyperlink).DataContext as Person).Nation);
		}

		private void RenameTacticButton_Click(object sender, RoutedEventArgs e)
		{
			RenameWindow renameWindow = new RenameWindow();
			if (renameWindow.ShowDialog() == true)
				(this.TacticsList.SelectedItem as Tactic).Name = renameWindow.NewName;
		}

		private void PlayerControl_Loaded(object sender, RoutedEventArgs e)
		{
			PlayerControl playerControl = (sender as PlayerControl);
			if (playerControl.Number >= 1 && playerControl.Number <= 10)
				playerControl.ToolTip = new ToolTip()
				{
					Content = "Левая кнопка мыши - перетаскивание игрока на новую позицию\nПравая кнопка мыши - дополнительное смещение"
				};
		}

		private void PositionInfoList_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender != null)
			{
				PositionInfo[] positionsArray = null;
				if (this.TacticsList.SelectedItem != null)
					positionsArray = (this.TacticsList.SelectedItem as Tactic).PositionInfoes;
				else
					positionsArray = (this.DataContext as Club).CurrentTactic.PositionInfoes;
				for (int I = 0; I < 11; I++)
					positionsArray[I].Number = I;
				this.PositionInfoList.ItemsSource = positionsArray;
			}
		}

		private void TacticsList_Loaded(object sender, RoutedEventArgs e)
		{
			this.TacticsList.SelectedItem = (this.DataContext as Club).CurrentTactic;
			this.PositionInfoList_Loaded(this.PositionInfoList, null);
		}






	}
}
