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
using Microsoft.Win32;
using ClassLibrary;
using ClassLibrary.DBClass;

namespace VNNG2013.Pages
{
	/// <summary>
	/// Логика взаимодействия для MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.MainWindow = (MainWindow)Window.GetWindow(this);
		}

		/// <summary> Основное окно приложения </summary>
		public MainWindow MainWindow;

		/// <summary> Хранилище Данных приложения </summary>
		public DataStorage DataStorage
		{
			get
			{
				return this.MainWindow.DataStorage;
			}
			set
			{
				this.MainWindow.DataStorage = value;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			if (openDialog.ShowDialog() == true)
			{
				this.DataStorage = new DataStorage(openDialog.FileName);
				DataGenerator gen = new DataGenerator(this.DataStorage);
				gen.Generate();
				Competition competition = this.DataStorage.GetDBObject<Competition>(1);
				this.MainWindow.Frame.Navigate(new CompetitionPage(competition));
			}
		}
	}
}
