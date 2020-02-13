using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using ClassLibrary.Attributes;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using ClassLibrary.DBClass;

namespace ClassLibrary
{
	/// <summary>
	/// Класс, содержащий статические методы преобразования
	/// </summary>
	public static class Helper
	{
		/// <summary> Выполняет переход на страницу для отображения данных для объекта "source" </summary>
		public static void NavigateTo(this Frame frame, DBObject source)
		{
			Type sourceTypePage = Type.GetType(string.Format("VNNG2013.Pages.{0}Page", source.GetType().Name));
			object page = Activator.CreateInstance(sourceTypePage, source);
			frame.Navigate(page);
		}
	}
}