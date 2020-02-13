using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ClassLibrary;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using ClassLibrary.DBClass;
using ClassLibrary.Attributes;
using System.Windows.Controls;

namespace DataEditor
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

		public DataStorage dataStorage { get; set; }

		/// <summary> Список всех объектов, имеющих тип, выбранный в EntitiesList </summary>
		public List<DBObject> Objects { get; set; }
		


		public bool IsDataStorageLoaded()
		{
			return (this.dataStorage != null);
		}






		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			WindowProperties.SetProperties(this);
		}









		private void OpenFile(object sender, ExecutedRoutedEventArgs e)
		{
			SpecialPaths.VerifyDirectory(SpecialPaths.DataBases);
			
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "sqlite files|*.sqlite|all files|*.*";
			openFileDialog.InitialDirectory = SpecialPaths.DataBases;
			
			if (openFileDialog.ShowDialog() == true)
			{
				//Начало загрузки данных
				this.BusyIndicator.IsBusy = true;
				this.BusyIndicator.BusyContent = "Загрузка данных...";
				
				BackgroundWorker worker = new BackgroundWorker();
				
				worker.DoWork += delegate
				{
					try
					{
						Dispatcher.Invoke((Action)(() =>
						                           {
						                           	this.dataStorage = new DataStorage(openFileDialog.FileName, true);
						                           	this.ShowEntities();
						                           }));
					}
					catch (Exception ex)
					{
						this.dataStorage = null;
						MessageBox.Show("Не удалось считать данные!\n\nException:\n" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}

				};
				
				worker.RunWorkerCompleted += delegate
				{
					//Загрузка завершена
					this.BusyIndicator.IsBusy = false;
				};
				
				worker.RunWorkerAsync();
			}
		}

		private void CanSave(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = this.IsDataStorageLoaded();
		}

		private void SaveFile(object sender, ExecutedRoutedEventArgs e)
		{
			SpecialPaths.VerifyDirectory(SpecialPaths.DataBases);
			
			//Начало сохранения данных
			this.BusyIndicator.BusyContent = "Сохранение данных...";
			this.BusyIndicator.IsBusy = true;
			
			BackgroundWorker worker = new BackgroundWorker();
			
			worker.DoWork += delegate
			{
				this.dataStorage.Save();
			};
			
			worker.RunWorkerCompleted += delegate
			{
				//Сохранение завершено
				this.BusyIndicator.IsBusy = false;
				
				MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			};
			
			worker.RunWorkerAsync();
			

		}

		private void SaveFileAs(object sender, ExecutedRoutedEventArgs e)
		{
			SpecialPaths.VerifyDirectory(SpecialPaths.DataBases);
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "SQLite file|*.sqlite";
			saveFileDialog.InitialDirectory = SpecialPaths.DataBases;

			if (saveFileDialog.ShowDialog(this) == true)
			{
				//Начало сохранения данных
				this.BusyIndicator.BusyContent = "Сохранение данных...";
				this.BusyIndicator.IsBusy = true;

				BackgroundWorker worker = new BackgroundWorker();
				
				worker.DoWork += delegate
				{
					this.dataStorage.SaveAs(saveFileDialog.FileName);
				};
				
				worker.RunWorkerCompleted += delegate
				{
					//Сохранение завершено
					this.BusyIndicator.IsBusy = false;

					MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				};
				
				worker.RunWorkerAsync();
			}
		}

		private void SaveToNewFileMenuItem_Click(object sender, RoutedEventArgs e)
		{
			const string _dateFormat = @"yyyy.MM.dd HH.mm.ss";

			string fileName = string.Format("{0}\\{1} - {2}.sqlite", SpecialPaths.DataBases, "db_vnng2013", DateTime.Now.ToString(_dateFormat));

			//Начало сохранения данных
			this.BusyIndicator.BusyContent = "Сохранение данных...";
			this.BusyIndicator.IsBusy = true;

			BackgroundWorker worker = new BackgroundWorker();
			
			worker.DoWork += delegate
			{
				this.dataStorage.SaveAs(fileName);
			};
			
			worker.RunWorkerCompleted += delegate
			{
				//Сохранение завершено
				this.BusyIndicator.IsBusy = false;
				
				MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			};
			
			worker.RunWorkerAsync();
		}

		private void ExitApplication(object sender, ExecutedRoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary> Код обработчика закрытия окна </summary>
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show(this, "Сохранить данные перед выходом из программы?", "Сохранить?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
			if (messageBoxResult == MessageBoxResult.Cancel)
			{
				e.Cancel = true;
				return;
			}
			else if (messageBoxResult == MessageBoxResult.Yes)
			{
				this.SaveToNewFileMenuItem_Click(null, null);
			}

			WindowProperties.SaveProperties(this);
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			ModifierKeys modifierKeys = e.KeyboardDevice.Modifiers;
			//На закрытие приложения
			if (modifierKeys.HasFlag(ModifierKeys.Control) && modifierKeys.HasFlag(ModifierKeys.Shift) && e.Key == Key.E)
			{
				this.ExitApplication(this, null);
			}
			//На свёртывание-развёртывание окна
			if (modifierKeys.HasFlag(ModifierKeys.Control) && e.Key == Key.Enter)
			{
				if (this.WindowStyle == System.Windows.WindowStyle.SingleBorderWindow)
					this.ToMaximizedWindow(null, null);
				else if (this.WindowStyle == System.Windows.WindowStyle.None)
					this.ToNormalWindow(null, null);
			}
			//На сохранение в новый файл
			if (modifierKeys.HasFlag(ModifierKeys.Control) && e.Key == Key.Q)
			{
				this.SaveToNewFileMenuItem_Click(null, null);
			}
		}






		/// <summary> Развернуть окно на весь экран </summary>
		private void ToMaximizedWindow(object sender, EventArgs e)
		{
			this.WindowState = System.Windows.WindowState.Maximized;
			this.WindowStyle = System.Windows.WindowStyle.None;
		}

		/// <summary> Вернуть нормальный вид окна </summary>
		private void ToNormalWindow(object sender, EventArgs e)
		{
			this.WindowState = System.Windows.WindowState.Normal;
			this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
		}






		/// <summary> Отображает все типы объектов в EntitiesList </summary>
		private void ShowEntities()
		{
			this.EntitiesList.ItemsSource = this.dataStorage.GetEntities().Where(x => x.EntityType.GetAttribute<EntityAttribute>().IsIndependent).OrderBy(x => x.TranslationName);
			this.ObjectsList.ItemsSource = null;
		}

		/// <summary> Просматреть объекты других типов </summary>
		public void EntitiesList_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.IsDataStorageLoaded() && this.EntitiesList.SelectedValue != null)
			{
				this.FilterTextBox.Text = string.Empty;
				this.Objects = this.dataStorage.GetDBObjects((DBEntity)this.EntitiesList.SelectedValue);
				this.ObjectsList.ItemsSource = this.Objects;
				if (this.ObjectsList.ItemsSource != null)
				{
					this.EditingArea.ReBuildEditingArea((DBEntity)this.EntitiesList.SelectedValue);
					this.ObjectsList.SelectedIndex = 0;
				}
			}
		}

		private void ApplyFiltering(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (this.Objects == null) return;

			this.ObjectsList.ItemsSource = this.Objects.Where(x => x.Contains(this.FilterTextBox.Text)).ToList();
		}

		private void ObjectsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (this.ObjectsList.SelectedValue != null)
			{
				this.EditingArea.DataContext = this.ObjectsList.SelectedValue;
			}
		}

		private void CreateDBObject_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsDataStorageLoaded())
			{
				DBObject dbObject = this.dataStorage.CreateDBObject((DBEntity)this.EntitiesList.SelectedValue);
				this.ObjectsList.ItemsSource = this.Objects;
				this.ApplyFiltering(null, null);
			}
		}

		private void DeleteDBObject_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsDataStorageLoaded())
			{
				if (this.ObjectsList.SelectedValue != null)
				{
					if (MessageBox.Show(string.Format("Вы действительно хотите удалить '{0}'", this.ObjectsList.SelectedValue.ToString()),
					                    "Удалить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						DBObject deletingObject = this.ObjectsList.SelectedValue as DBObject;
						this.dataStorage.DeleteDBObject(deletingObject);
						this.Objects.Remove(deletingObject);
						this.ObjectsList.ItemsSource = this.Objects;
						this.ApplyFiltering(null, null);
					}
				}
			}
		}

	}
}
