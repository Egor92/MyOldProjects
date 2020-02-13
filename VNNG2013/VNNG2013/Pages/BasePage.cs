using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ClassLibrary.DBClass;
using System.Windows;
using ClassLibrary;

namespace VNNG2013.Pages
{
	public class BasePage : Page
	{
		public BasePage() { }

		public BasePage(DBObject dataContext)
		{
			this.DataContext = dataContext;
			this.Loaded += Page_Loaded;
		}

		protected void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.MainWindow = (MainWindow)Window.GetWindow(this);
		}

		/// <summary> Основное окно приложения </summary>
		protected MainWindow MainWindow;

		/// <summary> Хранилище Данных приложения </summary>
		protected DataStorage DataStorage
		{
			get { return this.MainWindow.DataStorage; }
		}

		/// <summary> Выполняет переход на страницу для отображения данных для объекта "source" </summary>
		protected void Navigate(DBObject source)
		{
			Type sourceTypePage = Type.GetType(string.Format("VNNG2013.Pages.{0}Page", source.GetType().Name));
			object page = Activator.CreateInstance(sourceTypePage, source);
			this.MainWindow.Frame.Navigate(page);
		}

	}
}
