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
	/// Логика взаимодействия для ColorRibbon.xaml
	/// </summary>
	public partial class ColorRibbon : UserControl, INotifyPropertyChanged
	{
		public ColorRibbon()
		{
			InitializeComponent();

			CreateDefinitions();
			CreateColorBlocks();
		}

		private UIElementCollection Children
		{
			get { return RootGrid.Children; }
		}

		private void CreateDefinitions()
		{
			RootGrid.ColumnDefinitions.Clear();
			for (int i = 0; i < 256; i++)
				RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
		}

		private void CreateColorBlocks()
		{
			Children.Clear();
			for (int i = 0; i < 256; i++)
			{
				var colorBlock = new Border {Tag = i, Margin = new Thickness(-1.0,0.0,-1.0,0.0)};
				Grid.SetColumn(colorBlock, i);
				Children.Add(colorBlock);
			}
		}

		public bool IsHighlight { get; set; }

		private Spectr _spectr = Spectr.Red;
		public Spectr Spectr
		{
			get { return _spectr; }
			set 
			{ 
				_spectr = value;
				RepaintRibbon();
			}
		}

		public Color TargetColor
		{
			get { return (Color)GetValue(TargetColorProperty); }
			set
			{
				if (TargetColor == value)
					return;
				SetValue(TargetColorProperty, value);
				RepaintRibbon();
				OnPropertyChanged();
			}
		}

		public static readonly DependencyProperty TargetColorProperty = DependencyProperty.Register("TargetColor", 
																									typeof (Color), 
																									typeof (ColorRibbon),
																									new PropertyMetadata(Colors.Black));

		private void RepaintRibbon()
		{
			foreach (Border border in Children)
			{
				var color = TargetColor;
				switch (Spectr)
				{
					case Spectr.Red:
						color.R = Convert.ToByte(border.Tag);
						break;
					case Spectr.Green:
						color.G = Convert.ToByte(border.Tag);
						break;
					case Spectr.Blue:
						color.B = Convert.ToByte(border.Tag);
						break;
				}
				if (IsHighlight)
				{
					if (Color.AreClose(color, TargetColor))
						border.Background = Brushes.BlueViolet;
					else
						border.Background = Brushes.DarkGray;
				}
				else
					border.Background = new SolidColorBrush(color);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		private bool ColorsAreCloseBy(Color color1, Color color2, double radius)
		{
			int differenceRed = color1.R - color2.R;
			int differenceGreen = color1.G - color2.G;
			int differenceBlue = color1.B - color2.B;
			return differenceRed*differenceRed + differenceGreen*differenceGreen + differenceBlue*differenceBlue <= radius*radius;
		}
	}
}
