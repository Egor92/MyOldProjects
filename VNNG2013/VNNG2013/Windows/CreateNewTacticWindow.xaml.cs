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
using System.Windows.Shapes;
using ClassLibrary.DBClass;
using ClassLibrary.Resources.Tactics;
using VNNG2013.Resources.Controls;
using ClassLibrary.Resources.Tactics.Classes;

namespace VNNG2013.Windows
{
	/// <summary>
	/// Логика взаимодействия для CreateNewTacticWindow.xaml
	/// </summary>
	public partial class CreateNewTacticWindow : Window
	{
		public CreateNewTacticWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.currentTacticKey = 0;
			this.currentDefFormationKey = 0;
			this.currentMidFormationKey = 0;
			this.currentStrFormationKey = 0;
		}

		private int currentTacticKey;

		private int currentDefFormationKey;

		private int currentMidFormationKey;

		private int currentStrFormationKey;

		public GeneralTactic CurrentGeneralTactic
		{
			get
			{
				return StandartTactics.GeneralTactics[this.currentTacticKey];
			}
		}

		private void ToChangeStandartTactic(object sender, RoutedEventArgs e)
		{
			int standartTacticsCount = StandartTactics.GeneralTactics.Count();

			int summand;
			if ((sender as Button).Name == "ToUsePreviewStandartTacticButton")
				summand = standartTacticsCount - 1;
			else
				summand = 1;

			currentTacticKey = (currentTacticKey + summand) % standartTacticsCount;

			currentDefFormationKey = 0;
			currentMidFormationKey = 0;
			currentStrFormationKey = 0;

			this.DefFormationTextButton.Content = this.CurrentGeneralTactic.DefFormations[0].Text;
			this.MidFormationTextButton.Content = this.CurrentGeneralTactic.MidFormations[0].Text;
			this.StrFormationTextButton.Content = this.CurrentGeneralTactic.StrFormations[0].Text;

			this.RefreshStandartTacicInformation();
			this.RefreshPlayerPlacemantLocations();
		}

		private void RefreshStandartTacicInformation()
		{
			this.CurrentTacticTextBlock.Text = this.CurrentGeneralTactic.Name;
		}

		private void RefreshPlayerPlacemantLocations()
		{
			List<Point> locations = this.CurrentGeneralTactic
				.GetLocations(this.currentDefFormationKey, this.currentMidFormationKey, this.currentStrFormationKey);
			foreach (PlayerControl playerControl in this.PlayerControlsList.ItemsSource)
			{
				Canvas.SetLeft(playerControl, locations[playerControl.Number].X - PlayerControl.Radius);
				Canvas.SetTop(playerControl, locations[playerControl.Number].Y - PlayerControl.Radius);
			}
		}

		private void ToChangeDefFormation(object sender, RoutedEventArgs e)
		{
			this.currentDefFormationKey = (this.currentDefFormationKey + 1) % this.CurrentGeneralTactic.DefFormations.Count();
			(sender as ContentControl).Content = this.CurrentGeneralTactic.DefFormations[this.currentDefFormationKey].Text;
			this.RefreshPlayerPlacemantLocations();
		}

		private void ToChangeMidFormation(object sender, RoutedEventArgs e)
		{
			this.currentMidFormationKey = (this.currentMidFormationKey + 1) % this.CurrentGeneralTactic.MidFormations.Count();
			(sender as ContentControl).Content = this.CurrentGeneralTactic.MidFormations[this.currentMidFormationKey].Text;
			this.RefreshPlayerPlacemantLocations();
		}

		private void ToChangeStrFormation(object sender, RoutedEventArgs e)
		{
			this.currentStrFormationKey = (this.currentStrFormationKey + 1) % this.CurrentGeneralTactic.StrFormations.Count();
			(sender as ContentControl).Content = this.CurrentGeneralTactic.StrFormations[this.currentStrFormationKey].Text;
			this.RefreshPlayerPlacemantLocations();
		}

	}
}
