using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace ClassLibrary
{
	public static class WindowProperties
	{
		/// <summary> Сохраняет свойства окна в файл </summary>
		public static void SaveProperties(Window window, string fileName = "WindowProperties.xml")
		{
			XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.Default);
			writer.WriteStartElement("WindowProperties");
				writer.WriteStartElement("WindowStyle");
					writer.WriteValue(window.WindowStyle.ToString());
				writer.WriteEndElement();
				writer.WriteStartElement("WindowState");
					writer.WriteValue(window.WindowState.ToString());
				writer.WriteEndElement();
				writer.WriteStartElement("Height");
					writer.WriteValue(window.Height);
				writer.WriteEndElement();
				writer.WriteStartElement("Width");
					writer.WriteValue(window.Width);
				writer.WriteEndElement();
				writer.WriteStartElement("Left");
					writer.WriteValue(window.Left);
				writer.WriteEndElement();
				writer.WriteStartElement("Top");
					writer.WriteValue(window.Top);
				writer.WriteEndElement();
			writer.WriteEndElement();
			writer.Close();
		}

		/// <summary> 
		/// Читает свойства окна из файла и применяет их к окну. 
		/// Вызывать данный метод нужно в методе FrameworkElement.OnInitialized()
		/// </summary>
		public static void SetProperties(Window window, string fileName = "WindowProperties.xml")
		{
			window.Visibility = System.Windows.Visibility.Collapsed;
			string filePath = string.Format(".\\{0}", fileName);
			if (File.Exists(filePath))
			{
				XmlDocument reader = new XmlDocument();
				reader.Load(filePath);
				foreach (XmlNode node in reader.DocumentElement.ChildNodes) 
				{
					string propertyName = node.Name;
					string propertyValue = node.InnerText;
					PropertyInfo propertyInfo = typeof(Window).GetProperty(propertyName);
					if (propertyInfo.PropertyType.IsEnum)
					{
						FieldInfo fieldInfo = propertyInfo.PropertyType.GetField(propertyValue);
						object value = fieldInfo.GetValue(propertyInfo.GetValue(window, null));
						propertyInfo.SetValue(window, value, null);
					}
					else
						propertyInfo.SetValue(window, System.Convert.ChangeType(propertyValue, propertyInfo.PropertyType), null);
				}
			}
			window.Visibility = System.Windows.Visibility.Visible;
		}
	}
}
