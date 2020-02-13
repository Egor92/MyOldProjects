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
using ClassLibrary.DBClass;

namespace DataEditor
{
	/// <summary>
	/// Логика взаимодействия для SelectionWindow.xaml
	/// </summary>
	public partial class SelectionWindow : Window
	{
		private List<DBObject> objects;

		public SelectionWindow(List<DBObject> objects)
		{
			InitializeComponent();
			this.objects = objects;
			this.ObjectsList.ItemsSource = this.objects;
		}

		public DBObject DBObject
		{
			get
			{
				return this.ObjectsList.SelectedValue as DBObject;
			}
		}

		private void Reset_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			if (this.ObjectsList.SelectedValue != null)
			{
				this.DialogResult = true;
			}
			else
			{
				MessageBox.Show("Объект не выбран!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		private void ObjectsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.OK_Click(null, null);
		}

		private void ApplyFiltering(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (this.objects == null) return;

			this.ObjectsList.ItemsSource = this.objects.Where(x => x.Contains(this.FilterTextBox.Text)).ToList();
		}
	}
}
