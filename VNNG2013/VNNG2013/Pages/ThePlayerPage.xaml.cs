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
using ClassLibrary;
using ClassLibrary.DBClass;

namespace VNNG2013.Pages
{
	/// <summary>
	/// Логика взаимодействия для ThePlayerPage.xaml
	/// </summary>
	public partial class ThePlayerPage : Page
	{
		public ThePlayerPage()
		{
			InitializeComponent();
		}

		/// <summary> Основное окно приложения </summary>
		public MainWindow MainWindow = (Application.Current as App).MainWindow as MainWindow;

		/// <summary> Навигационное окно приложения </summary>
		public Frame Frame = ((Application.Current as App).MainWindow as MainWindow).Frame;

		/// <summary> Хранилище Данных приложения </summary>
		public DataStorage DataStorage
		{
			get
			{
				return ((Application.Current as App).MainWindow as MainWindow).DataStorage;
			}
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			if (this.DataStorage.GetDBObjects(typeof(ThePlayer).GetEntity()).Count == 0)
			{
				
			}
		}

		private void CreateNewManager(object sender, RoutedEventArgs e)
		{
			Person manager = new Person()
			{
				Name = this.Name_TextBox.Text,
				Surname = this.Surname_TextBox.Text,
				BirthdayDate = this.Birthday_DatePicker.SelectedDate.Value,
				Type = Person.PersonType.Manager,
			};
			ThePlayer player = new ThePlayer();
		}
	}
}
