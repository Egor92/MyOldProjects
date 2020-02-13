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
	/// Логика взаимодействия для PersonPage.xaml
	/// </summary>
	public partial class PersonPage : BasePage
	{
		public PersonPage(Person person) : base(person)
		{
			InitializeComponent();
		}

		private void ToNationClick(object sender, RoutedEventArgs e)
		{
			this.Navigate(((sender as Hyperlink).DataContext as Person).Nation);
		}



	}
}
