using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Reflection;
using ClassLibrary;
using ClassLibrary.DBClass;
using System.ComponentModel;
using ClassLibrary.Converters;
using System.Windows.Data;
using System;
using System.Windows.Media.Imaging;

namespace DataEditor
{
	/// <summary>
	/// Логика взаимодействия для DBObjectSelector.xaml
	/// </summary>
	public partial class DBObjectSelector : Button
	{
		/// <summary> Ссылка на Хранилище Данных </summary>
		private DataStorage DataStorage = ((Application.Current as App).MainWindow as MainWindow).dataStorage;

		/// <summary> Свойство, значение которого должно отображаться на кнопке </summary>
		private PropertyInfo property;
		[TypeConverter(typeof(StringToPropertyConverter))]
		public PropertyInfo Property
		{
			get { return this.property; }
			set
			{
				this.property = value;
				Binding binding = new Binding(this.property.Name+".DisplayedText");
				BindingOperations.SetBinding(this, ContentControl.ContentProperty, binding);
			}
		}

		public DBObjectSelector()
		{
			InitializeComponent();
		}

		public DBObjectSelector(PropertyInfo property)
		{
			InitializeComponent();
			this.Property = property;
		}

		void ChangeDBObject_MouseRightDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.DataContext != null)
			{
				ContextMenu contextMenu = new ContextMenu();

				MenuItem item;

				//Объект, хранящийся в свойстве DBObjectSelector.Property
				DBObject propertyValue = this.Property.GetValue(this.DataContext, null) as DBObject;
				if (propertyValue != null)
				{
					//Если объект существует, то создадим ссылку на его редактирование в контекстном меню
					item = new MenuItem() { Foreground = Brushes.BlueViolet };
					item.Icon = new Image() { Source = new BitmapImage(new Uri("..\\..\\Images\\MoveArrow.png", UriKind.Relative)), Width = 20, Height = 20 };
					item.Header = new TextBlock() { Text = "Перейти", Foreground = Brushes.DarkGreen };
					item.Click += (s, eventArgs) =>
					{
						MainWindow mainWindow = (Application.Current as App).MainWindow as MainWindow;
						mainWindow.EntitiesList.SelectedItem = (mainWindow.EntitiesList.ItemsSource as IEnumerable<DBEntity>).Single(x => x.EntityType == propertyValue.GetType());
						//Событие обрабатывается на сразу, а только при двойном нажатии
						mainWindow.EntitiesList_DoubleClick(null, null);
						mainWindow.ObjectsList.SelectedItem = (mainWindow.ObjectsList.ItemsSource as IEnumerable<DBObject>).Single(x => x.ID == propertyValue.ID);
					};
					contextMenu.Items.Add(item);
				}

				item = new MenuItem() { Header = "Сбросить значение", Foreground = Brushes.Red };
				item.Icon = new Image() { Source = new BitmapImage(new Uri("..\\..\\Images\\Delete.png", UriKind.Relative)), Width = 20, Height = 20 };
				item.Click += (s, eventArgs) =>
				{
					this.Property.SetValue(this.DataContext, null, null);
				};
				contextMenu.Items.Add(item);

				DataStorage dataStorage = this.DataStorage;
				DBEntity entity = new DBEntity(this.Property.PropertyType);
				List<DBObject> objects = dataStorage.GetDBObjects(entity);
				foreach (DBObject dbObject in dataStorage.GetFromCache(entity).ApplyConditions(this.Property))
				{
					item = new MenuItem() { DataContext = dbObject, Header = dbObject.ToString() };
					item.Click += (s, eventArgs) =>
					{
						this.Property.SetValue(this.DataContext, (s as MenuItem).DataContext, null);
						dataStorage.AddToCache((s as MenuItem).DataContext as DBObject);
					};
					contextMenu.Items.Add(item);
				}

				(sender as Button).ContextMenu = contextMenu;
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		void ChangeDBObject_Click(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{
				DBEntity entity = new DBEntity(this.Property.PropertyType);
				List<DBObject> objects = this.DataStorage.GetDBObjects(entity).ApplyConditions(this.Property);
				SelectionWindow SelectionWindow = new SelectionWindow(objects);

				if (SelectionWindow.ShowDialog() == true)
				{
					this.Property.SetValue(this.DataContext, SelectionWindow.DBObject, null);
					this.DataStorage.AddToCache(SelectionWindow.DBObject);
				}
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

	}
}
