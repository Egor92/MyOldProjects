using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	abstract class BaseObject : INotifyPropertyChanged
	{
		private string _text;
		public string Text 
		{ 
			get
			{
				if (string.IsNullOrWhiteSpace(_text))
					return DefaultText;
				return _text;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					_text = DefaultText;
				else
					_text = value;
				OnPropertyChanged("Text");
			}
		}

		protected abstract string DefaultText { get; }

		protected abstract Color ObjectColor { get; }

		public SolidColorBrush Background
		{
			get { return new SolidColorBrush(ObjectColor); }
		}

		public SolidColorBrush Foreground
		{
			get
			{
				Color color = Color.Multiply(ObjectColor, 0.5f);
				color.A = 255;
				return new SolidColorBrush(color);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
