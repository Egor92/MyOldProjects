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
    /// Логика взаимодействия для CountryPage.xaml
    /// </summary>
    public partial class CountryPage : Page
    {
        public CountryPage()
        {
            InitializeComponent();
        }
        public CountryPage(int code)
        {
            InitializeComponent();
            Country p = DataBase.GetCountry(code); ;
            this.DataContext = p;
            this.Title = p.Name;
        }

        private void LinkToChampionship_Click(object sender, RoutedEventArgs e)
        {
            ChampionshipPage p = new ChampionshipPage(((sender as Hyperlink).DataContext as Championship).Code);
            NavigationService.Navigate(p);
        }

        private void ChampionshipList_Loaded(object sender, RoutedEventArgs e)
        {
            ChampionshipList.ItemsSource = (this.DataContext as Country).Championships;
        }

    }
}
