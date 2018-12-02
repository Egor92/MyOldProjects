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
using VNNG2011;

namespace Volga_NN_Gamer_2011
{
    /// <summary>
    /// Логика взаимодействия для ChampionshipPage.xaml
    /// </summary>
    public partial class ChampionshipPage : Page
    {
        public ChampionshipPage()
        {
            InitializeComponent();
        }

        public ChampionshipPage(int code)
        {
            InitializeComponent();
            this.DataContext = DataBase.GetChampionship(code);
            this.Title = (this.DataContext as Championship).Name;
        }

        private void ClubsList_Loaded(object sender, RoutedEventArgs e)
        {
            this.ClubsList.ItemsSource = (this.DataContext as Championship).Clubs;
        }

        private void LinkToClub_Click(object sender, RoutedEventArgs e)
        {
            ClubPage p = new ClubPage(((sender as Hyperlink).DataContext as Club).Code);
            NavigationService.Navigate(p);
        }

        private void LinkToCountry_Click(object sender, RoutedEventArgs e)
        {
            CountryPage p = new CountryPage(((sender as Hyperlink).DataContext as Championship).Country.Code);
            NavigationService.Navigate(p);
        }

    }
}
