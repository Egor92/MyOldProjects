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

namespace VNNG2013.Windows
{
	/// <summary>
	/// Логика взаимодействия для RenameWindow.xaml
	/// </summary>
	public partial class RenameWindow : Window
	{
		public RenameWindow()
		{
			InitializeComponent();
		}

		public string NewName
		{
			get { return this.NewNameTextBox.Text.Trim(); }
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.NewName))
				MessageBox.Show("Пустая строка не может быть именем", "Внимание!", MessageBoxButton.OK);
			else
				this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
