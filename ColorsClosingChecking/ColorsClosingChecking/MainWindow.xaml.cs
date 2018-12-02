using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorsClosingChecking.Annotations;

namespace ColorsClosingChecking
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public Color CheckingColor
		{
			get
			{
				return new Color()
					{
						A = 255,
						R = (byte)this.RedValueSlider.Value,
						G = (byte)this.GreenValueSlider.Value,
						B = (byte)this.BlueValueSlider.Value
					};
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		private void CheckingColorSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			OnPropertyChanged("CheckingColor");
			/*BrightColorRibbon.TargetColor = CheckingColor;
			HighlightColorRibbon.TargetColor = CheckingColor;*/
		}

		private void Spectr_OnChecked(object sender, SelectionChangedEventArgs e)
		{
			switch (((ComboBox)sender).SelectedValue.ToString())
			{
				case "Red":
					BrightColorRibbon.Spectr = Spectr.Red;
					HighlightColorRibbon.Spectr = Spectr.Red;
					break;
				case "Green":
					BrightColorRibbon.Spectr = Spectr.Green;
					HighlightColorRibbon.Spectr = Spectr.Green;
					break;
				case "Blue":
					BrightColorRibbon.Spectr = Spectr.Blue;
					HighlightColorRibbon.Spectr = Spectr.Blue;
					break;
			}
		}
	}
}
