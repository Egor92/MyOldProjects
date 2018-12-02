using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FunWithDataTemplate
{
	class CustomTemplateSelector : DataTemplateSelector
	{
		public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			Person person = item as Person;
			if (person == null) return null;

			Window window = Application.Current.MainWindow;

			if (person.TemplateType == TemplateType.Strict)
				return window.FindResource("StrictTemplate") as DataTemplate;
			if (person.TemplateType == TemplateType.Bright)
				return window.FindResource("BrightTemplate") as DataTemplate;

			return base.SelectTemplate(item, container);
		}
	}
}
