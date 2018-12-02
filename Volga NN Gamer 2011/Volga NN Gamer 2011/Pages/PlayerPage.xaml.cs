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
    /// Логика взаимодействия для PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        public PlayerPage(int code)
        {
            InitializeComponent();
            Player p = DataBase.GetPlayer(code);
            this.DataContext = p;
            this.Title = p.Info.Name + " " + p.Info.Surname;
            //p.PhysicalAttr.Power = 55;
            //p.PhysicalAttr.Save();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //ClubPage p = new ClubPage(DataBase.GetClubs("[Полное название] = '" + (sender as Hyperlink).TargetName + "'")[0].Code);
            ClubPage p = new ClubPage((this.DataContext as Player).Info.Club.Code);
            NavigationService.Navigate(p);
        }

        private void LinkToFirstCountry_Click(object sender, RoutedEventArgs e)
        {
            CountryPage p = new CountryPage(((sender as Hyperlink).DataContext as Player).Info.FirstNation.Code);
            NavigationService.Navigate(p);
        }

        private void LinkToSecondCountry_Click(object sender, RoutedEventArgs e)
        {
            CountryPage p = new CountryPage(((sender as Hyperlink).DataContext as Player).Info.SecondNation.Code);
            NavigationService.Navigate(p);
        }

      
    }
}
