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
using System.Windows.Media.Animation;
using VNNG2013.Pages;
using ClassLibrary.Interfaces;

namespace VNNG2013
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

		/// <summary> Хранилище Данных приложения </summary>
		public DataStorage DataStorage { get; set; }

		/// <summary> Предыдущая страница </summary>
		public Page lastPage;

		private void Frame_Navigated(object sender, NavigationEventArgs e)
		{
			this.DataContext = (this.Frame.Content as Page).DataContext;

			//Нельзя переходить на MainPage, так игра уже началась
			if (this.lastPage is MainPage)
				if (this.Frame.CanGoBack)
					this.Frame.RemoveBackEntry();
			
			this.lastPage = (Page)e.Content;
		}

		private void ToNavigateToUpLevel(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
				this.Frame.NavigateTo((this.DataContext as IWindowData).UpLevel);
		}

		private void GoToNextObject(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
				this.Frame.NavigateTo((this.DataContext as IWindowData).Next);
			var a = this.DataStorage.GetDBObjects<Tactic>()[0];
		}

		private void GoToPreviosObject(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
				this.Frame.NavigateTo((this.DataContext as IWindowData).Previous);
		}


	}
}
