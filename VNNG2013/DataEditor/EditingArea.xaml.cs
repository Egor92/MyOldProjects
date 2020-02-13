using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ClassLibrary;
using ClassLibrary.Attributes;
using System.Reflection;
using Xceed.Wpf.Toolkit;
using ClassLibrary.DBClass;
using System.Windows.Media;
using ClassLibrary.Converters;
using System.Windows.Controls.Primitives;

namespace DataEditor
{
	/// <summary>
	/// Логика взаимодействия для EditingArea.xaml
	/// </summary>
	public partial class EditingArea : UserControl
	{
		/// <summary> Ссылка на главное окно приложения </summary>
		private MainWindow MainMindow = (Application.Current as App).MainWindow as MainWindow;

		private LinearGradientBrush Brush
		{
			get
			{
				LinearGradientBrush brush = new LinearGradientBrush();
				brush.StartPoint = new Point(0, 0);
				brush.EndPoint = new Point(0, 1);
				brush.GradientStops.Add(new GradientStop(Colors.Lavender, 0.0));
				brush.GradientStops.Add(new GradientStop(Colors.LightSteelBlue, 0.8));
				brush.GradientStops.Add(new GradientStop(Colors.Lavender, 1.0));
				return brush;
			}
		}

		public EditingArea()
		{
			InitializeComponent();
		}

		/// <summary> Перестроить структуру EditingArea в связи с изменением типа изменяемых объектов </summary>
		public void ReBuildEditingArea(DBEntity entity)
		{
			this.ClearTabItems();
			this.BuildTabItems(entity.EntityType);
			this.FillEditingAreaWithProperties(entity.EntityType);
		}

		/// <summary> Уничтожает предыдущую структуру EditingArea </summary>
		private void ClearTabItems()
		{
			this.TabControl.ItemsSource = null;
		}

		/// <summary> Определяет список TabItem'ов и создаёт и добавляет их в EditingArea </summary>
		private void BuildTabItems(Type objectsType)
		{
			//Находим атрибут "ItemsCollectionAttribute"
			object[] customAttributes = objectsType.GetCustomAttributes(typeof(ItemsCollectionAttribute), false);
			//Если мы его 'забыли' приписать, то...
			if (customAttributes.Length == 0)
			{
				//...создаём один TabItem со StackPanel внутри...
				this.TabControl.ItemsSource = new TabItem[1] { new TabItem() { Header = "Информация", Content = new ScrollViewer() { 
						VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
						Content = new StackPanel() { Tag = 0 } 
						}}};

			}
			else
			{
				//...иначе всё в порядке! Просто изымаем коллекцию готовых TabItem'ов
				this.TabControl.ItemsSource = (customAttributes.First() as ItemsCollectionAttribute).GetItemsCollection();
			}
			this.TabControl.SelectedIndex = 0;
		}

		/// <summary> Заполняет EditingArea свойствами объекта для редактирования </summary>
		private void FillEditingAreaWithProperties(Type objectsType)
		{
			this.AddPropertyToEditingArea(objectsType.GetProperties<EditingPropertyAttribute>().Where(x => x.Name == "ID").First());

			IEnumerable<PropertyInfo> properties = objectsType.GetProperties<EditingPropertyAttribute>().Where(x => x.Name != "ID");
			foreach (PropertyInfo property in properties)
			{
				this.AddPropertyToEditingArea(property);
			}
		}

		/// <summary> Добавляет на EditingArea свойство объекта для редактирования </summary>
		private void AddPropertyToEditingArea(PropertyInfo property)
		{
			EditingPropertyAttribute attribute = property.GetAttribute<EditingPropertyAttribute>() as EditingPropertyAttribute;
			StackPanel stackPanel;
			if (attribute.IsInitializedByString)
				stackPanel = (this.TabControl.Items.GetTabItem(attribute.GetTabItemName()).Content as ScrollViewer).Content as StackPanel;
			else
			{
				stackPanel = ((this.TabControl.Items[attribute.GetTabItemNumber()] as TabItem).Content as ScrollViewer).Content as StackPanel;
			}
			stackPanel.Children.Add(this.CreatePanel(property));
		}

		/// <summary> Создаёт панель для редактирования свойства, создавая на ней необходимый типу свойства FrameworkElement </summary>
		private Panel CreatePanel(PropertyInfo property)
		{
			if (property.PropertyType.IsArray)
			{
				EntityAttribute entityAttribute = property.PropertyType.GetElementType().GetAttribute<EntityAttribute>() as EntityAttribute;
				return this.CreatePanelForBindedArrayProperty(property, entityAttribute.IsIndependent);
			}
			else
			{
				return this.CreatePanelForValueProperty(property);
			}
		}

		/// <summary> Создаёт панель для редактирования свойства, создавая на ней необходимый типу свойства FrameworkElement </summary>
		private Panel CreatePanelForValueProperty(PropertyInfo property)
		{
			Grid panel = new Grid() { Background = this.Brush };
			panel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			panel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

			TextBlock propertyNameTextBox = new TextBlock()
			{
				Text = (property.GetCustomAttributes(typeof(EditingPropertyAttribute), false).First() as EditingPropertyAttribute).PropertyTranslateName,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
				VerticalAlignment = System.Windows.VerticalAlignment.Center,
			};
			Grid.SetColumn(propertyNameTextBox, 0);
			panel.Children.Add(propertyNameTextBox);

			Control control = this.GetControl(property);

			if (control != null)
			{
				Grid.SetColumn(control, 1);

				try
				{
					this.SetBinding(property, control, panel);
				}
				catch
				{
					control = new Label()
					{
						Foreground = new SolidColorBrush(Colors.Red),
						Content = "Не удалось осуществить привязку даных!",
						HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
						VerticalAlignment = System.Windows.VerticalAlignment.Center
					};
				}
			}
			else
			{
				control = new Label() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Content = "Не определено!" };
			}

			panel.Children.Add(control);

			return panel;
		}

		/// <summary> Создаёт панель для редактирования привязанного свойства, имеющего тип "массив", редактируемого, как самостоятельный объект </summary>
		private Panel CreatePanelForBindedArrayProperty(PropertyInfo property, bool EntityIsIndependent)
		{
			StackPanel panel = new StackPanel() { Background = this.Brush };

			TextBlock propertyName = new TextBlock()
			{
				Text = (property.GetAttribute<EditingPropertyAttribute>() as EditingPropertyAttribute).PropertyTranslateName,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
			};
			panel.Children.Add(propertyName);

			ListView ObjectsList = new ListView()
			{
				Tag = property,
				ItemTemplate = ((DBEntity)this.MainMindow.EntitiesList.SelectedItem).GetDataTemplate(property)
			};
			if (EntityIsIndependent)
			{
				ObjectsList.MouseDoubleClick += (s, eventArgs) =>
					{
						if ((s as ListView).SelectedItem != null)
						{
							Type type = ((s as ListView).Tag as PropertyInfo).PropertyType.GetElementType();
							this.MainMindow.EntitiesList.SelectedItem = new DBEntity(type);
							this.MainMindow.EntitiesList_DoubleClick(null, null);
							this.MainMindow.ObjectsList.SelectedItem = (s as ListView).SelectedItem;
						}
					};
			}

			StackPanel commandsPanel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };

			try
			{
				this.SetBinding(property, ObjectsList, panel);

				Button AddButton = new Button() { Content = "Добавить в список", Width = 140, Margin = new Thickness(2), Tag = ObjectsList };
				Button DeleteButton = new Button() { Content = "Удалить из списка", Width = 140, Margin = new Thickness(2), Tag = ObjectsList };

				if (EntityIsIndependent)
				{
					AddButton.Click += new RoutedEventHandler(AddButtonForIndependent_Click);
					DeleteButton.Click += new RoutedEventHandler(DeleteButtonForIndependent_Click);
				}
				else
				{
					AddButton.Click += new RoutedEventHandler(AddButtonForNotIndependent_Click);
					DeleteButton.Click += new RoutedEventHandler(DeleteButtonForNotIndependent_Click);
				}

				commandsPanel.Children.Add(AddButton);
				commandsPanel.Children.Add(DeleteButton);
			}
			catch
			{
				ObjectsList.ItemsSource = null;
				ObjectsList.Items.Add(new ListViewItem() 
				{
					Foreground = new SolidColorBrush(Colors.Red),
					Content = "Не удалось осуществить привязку даных!"
				});
				commandsPanel = new StackPanel();
			}

			panel.Children.Add(ObjectsList);
			panel.Children.Add(commandsPanel);

			return panel;
		}

		/// <summary> Устанавливает привязку данных для свойства к указанному Control </summary>
		private void SetBinding(PropertyInfo property, Control control, Panel panel)
		{
			IsVisibleIfAttribute isVisibleIfAttribute = property.GetAttribute<IsVisibleIfAttribute>();
			if (isVisibleIfAttribute != null)
			{
				Binding isVisibleBinding = new Binding(isVisibleIfAttribute.PropertyName);
				isVisibleBinding.ConverterParameter = isVisibleIfAttribute.ValueOfIsVisible;
				isVisibleBinding.Mode = BindingMode.OneWay;
				isVisibleBinding.Converter = new ValueToVisibleConverter();
				BindingOperations.SetBinding(panel, Panel.VisibilityProperty, isVisibleBinding);
			}

			DependencyProperty dependencyProperty = this.GetDependencyProperty(property.PropertyType);
			if (!BindingOperations.IsDataBound(control, dependencyProperty))
			{
				Binding binding = new Binding(property.Name);
				if (property.PropertyType.Is<id>() || property.GetSetMethod() == null) binding.Mode = BindingMode.OneWay;
				if (property.PropertyType.IsEnum)
				{
					binding.Mode = BindingMode.OneWay;	//В другую сторону будет обрабатываться через код
					binding.Converter = new EnumToIntConverter();
				}

				BindingOperations.SetBinding(control, dependencyProperty, binding);
			}
		}

		/// <summary> Обработчик события кнопки для добавления нового объекта к привязанному свойству не самостоятельного объекта </summary>
		private void AddButtonForNotIndependent_Click(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{
				ListView objectsList = (sender as Button).Tag as ListView;
				Type entityType = (objectsList.Tag as PropertyInfo).PropertyType.GetElementType();
				DBObject newObject = this.MainMindow.dataStorage.CreateDBObject(new DBEntity(entityType));
				List<DBObject> newValue = objectsList.ItemsSource.Cast<DBObject>().ToList();
				newValue.Add(newObject);
				((objectsList.Tag) as PropertyInfo).SetValue(this.DataContext, newValue.ToArray().Cast(entityType), null);
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary> Обработчик события кнопки для удаления выбранного объекта у привязанному свойству не самостоятельного объекта </summary>
		private void DeleteButtonForNotIndependent_Click(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{
				DataStorage dataStorage = this.MainMindow.dataStorage;
				ListView objectsList = (sender as Button).Tag as ListView;
				if (objectsList.SelectedItem != null)
				{
					PropertyInfo propertyInfo = objectsList.Tag as PropertyInfo;
					Type type = propertyInfo.PropertyType.GetElementType();
					List<DBObject> newValue = objectsList.ItemsSource.Cast<DBObject>().ToList();
					this.MainMindow.dataStorage.DeleteDBObject(objectsList.SelectedItem as DBObject);
					newValue.Remove(objectsList.SelectedItem as DBObject);
					propertyInfo.SetValue(this.DataContext, newValue.Cast(type), null);
				}
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary> Обработчик события кнопки для добавления нового объекта к привязанному свойству самостоятельного объекта</summary>
		private void AddButtonForIndependent_Click(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{
				DataStorage dataStorage = this.MainMindow.dataStorage;
				ListView ObjectsList = (sender as Button).Tag as ListView;
				PropertyInfo propertyInfo = ObjectsList.Tag as PropertyInfo;
				Type type = propertyInfo.PropertyType.GetElementType();
				List<DBObject> objects = dataStorage.GetDBObjects(new DBEntity(type)).Except(ObjectsList.ItemsSource as DBObject[]).ApplyConditions(propertyInfo);

				SelectionWindow SelectionWindow = new SelectionWindow(objects);

				if (SelectionWindow.ShowDialog() == true)
				{
					List<DBObject> newValue = ObjectsList.ItemsSource.Cast<DBObject>().ToList();
					newValue.Add(SelectionWindow.DBObject);
					propertyInfo.SetValue(this.DataContext, newValue.ToArray().Cast(type), null);
					dataStorage.AddToCache(SelectionWindow.DBObject);
				}
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary> Обработчик события кнопки для удаления выбранного объекта у привязанному свойству самостоятельного объекта</summary>
		private void DeleteButtonForIndependent_Click(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{
				DataStorage dataStorage = this.MainMindow.dataStorage;
				ListView ObjectsList = (sender as Button).Tag as ListView;
				if (ObjectsList.SelectedItem != null)
				{
					PropertyInfo propertyInfo = ObjectsList.Tag as PropertyInfo;
					Type type = propertyInfo.PropertyType.GetElementType();
					List<DBObject> newValue = ObjectsList.ItemsSource.Cast<DBObject>().ToList();
					newValue.Remove(ObjectsList.SelectedItem as DBObject);
					propertyInfo.SetValue(this.DataContext, newValue.Cast(type), null);
				}
			}
			else
			{
				System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary> Добавляет в ComboBox коллекцию итемов, соответствующих значениям типа перечисления </summary>
		/// <param name="type"> Тип перечисления </param>
		private void AddItems(ComboBox comboBox, Type enumType)
		{
			foreach (FieldInfo fieldInfo in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				int key = (int)fieldInfo.GetValue(null);
				string value = (fieldInfo.GetAttribute<FieldTranslationAttribute>() as FieldTranslationAttribute).FieldTranslateName;
				comboBox.Items.Add(new EnumTuple(key, value));
			}
		}

		/// <summary> Возвращает необходимый свойству FrameworkElement </summary>
		private Control GetControl(PropertyInfo property)
		{
			Control Result = null;
			if (property.PropertyType == typeof(string))
			{
				LimitAttribute limitAttribute = property.GetAttribute<LimitAttribute>() as LimitAttribute;
				Result = new TextBox();
				if (limitAttribute != null)
				{
					(Result as TextBox).MaxLength = limitAttribute.UpLimit;
				}
			}
			else if (property.PropertyType == typeof(int))
			{
				LimitAttribute limitAttribute = property.GetAttribute<LimitAttribute>() as LimitAttribute;
				Result = new IntegerUpDown();
				if (limitAttribute != null)
				{
					(Result as IntegerUpDown).Minimum = limitAttribute.LowLimit;
					(Result as IntegerUpDown).Maximum = limitAttribute.UpLimit;
				}
			}
			else if (property.PropertyType == typeof(bool))
			{
				Result = new CheckBox();
			}
			else if (property.PropertyType == typeof(double))
			{
				Result = new DoubleUpDown();
			}
			else if (property.PropertyType == typeof(id))
			{
				Result = new TextBox() { IsEnabled = false };
			}
			else if (property.PropertyType == typeof(DateTime))
			{
				Result = new DatePicker();
			}
			else if (property.PropertyType.IsEnum)
			{
				Result = new ComboBox();
				(Result as ComboBox).SelectionChanged += (s, eventArgs) =>
					{
						if (DataContext != null)
						{
							if ((s as ComboBox).SelectedIndex != -1)
								property.SetValue(DataContext, new EnumToIntConverter().ConvertBack((s as ComboBox).SelectedIndex, property.PropertyType, null, null), null);
						}
						else
						{
							System.Windows.MessageBox.Show("Редактируемый объект не выбран!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
					};
				this.AddItems(Result as ComboBox, property.PropertyType);
			}
			else if (property.PropertyType == typeof(bool?))
			{
				Result = new ToggleButton() { IsThreeState = true };
				Binding binding = new Binding(property.Name);
				binding.Mode = BindingMode.OneWay;
				binding.Converter = new NullableBoolToStringConverter();
				BindingOperations.SetBinding(Result, ToggleButton.ContentProperty, binding);
			}
			else if (property.PropertyType.Is<DBObject>())
			{
				Result = new DBObjectSelector(property);
			}

			if (property.PropertyType != typeof(id))
			{
				EditingPropertyAttribute editingPropertyAttribute = property.GetAttribute<EditingPropertyAttribute>();
				Result.IsEnabled = !editingPropertyAttribute.IsReadOnly;
			}

			Result.MinHeight = 24;
			Result.VerticalAlignment = System.Windows.VerticalAlignment.Center;

			return Result;
		}

		/// <summary> Возвращает необходимый типу DependencyProperty, необходимый для привязки данных </summary>
		private DependencyProperty GetDependencyProperty(Type type)
		{
			DependencyProperty Result = null;
			if (type == typeof(string))
			{
				Result = TextBox.TextProperty;
			}
			else if (type == typeof(int))
			{
				Result = IntegerUpDown.ValueProperty;
			}
			else if (type == typeof(bool))
			{
				Result = CheckBox.IsCheckedProperty;
			}
			else if (type == typeof(double))
			{
				Result = DoubleUpDown.ValueProperty;
			}
			else if (type == typeof(id))
			{
				Result = TextBox.TextProperty;
			}
			else if (type == typeof(DateTime))
			{
				Result = DatePicker.SelectedDateProperty;
			}
			else if (type.IsEnum)
			{
				Result = ComboBox.SelectedIndexProperty;
			}
			else if (type == typeof(bool?))
			{
				Result = ToggleButton.IsCheckedProperty;
			}
			else if (type.Is<DBObject>())
			{
				Result = Button.ContentProperty;
			}
			else if (type.IsArray)
			{
				Result = ItemsControl.ItemsSourceProperty;
			}

			return Result;
		}

	}
}
