using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using ClassLibrary.Attributes;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using ClassLibrary.DBClass;
using System.Windows.Media;
using System.Windows.Markup;
using System.Net;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
	/// <summary>
	/// Класс, содержащий статические методы преобразования
	/// </summary>
	public static class Helper
	{
		/// <summary> Проверяет принадлежность свойства класса DBObject к атрибуту базы данных </summary>
		public static bool HasAttribute<TAttribute>(this MemberInfo property) where TAttribute : Attribute
		{
			return property.IsDefined(typeof(TAttribute), false);
		}

		/// <summary> Изымает атрибут данного свойства </summary>
		public static TAttribute GetAttribute<TAttribute>(this MemberInfo property) where TAttribute : Attribute
		{
			return property.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
		}

		/// <summary> Возвращает все поля и свойства класса, помеченные атрибутом DBAttribute </summary>
		public static List<MemberInfo> GetMembers<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(x => x.HasAttribute<TAttribute>() && (x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property))
				.ToList();
		}

		/// <summary> Метод GetValue для члена класса, имеющего тип FieldInfo или PropertyInfo </summary>
		public static object GetValue(this MemberInfo member, object obj)
		{
			if (member is FieldInfo)
				return (member as FieldInfo).GetValue(obj);
			else if (member is PropertyInfo)
				return (member as PropertyInfo).GetValue(obj, null);
			else
				throw new ArgumentException("Параметр member должен иметь тип FieldInfo или PropertyInfo");
		}

		/// <summary> Метод SetValue для члена класса, имеющего тип FieldInfo или PropertyInfo </summary>
		public static void SetValue(this MemberInfo member, object obj, object value)
		{
			if (member is FieldInfo)
				(member as FieldInfo).SetValue(obj, value);
			else if (member is PropertyInfo)
				(member as PropertyInfo).SetValue(obj, value, null);
			else
				throw new ArgumentException("Параметр member должен иметь тип FieldInfo или PropertyInfo");
		}

		/// <summary> Возвращает значение FieldInfo или PropertyInfo </summary>
		public static Type GetMemberType(this MemberInfo member)
		{
			if (member is FieldInfo)
				return (member as FieldInfo).FieldType;
			else if (member is PropertyInfo)
				return (member as PropertyInfo).PropertyType;
			else
				throw new ArgumentException("Параметр member должен иметь тип FieldInfo или PropertyInfo");
		}

		/// <summary> Возвращает все свойства класса, помеченные атрибутом DBAttribute </summary>
		public static List<PropertyInfo> GetProperties<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.HasAttribute<TAttribute>()).ToList();
		}

		public static DbType GetDbType(this Type type)
		{
			switch (type.Name)
			{
				case "String":
					return DbType.String;
				case "Int32":
					return DbType.Int32;
				case "Nullable<Boolean>":
					return DbType.Boolean;
				case "Boolean":
					return DbType.Boolean;
				case "DateTime":
					return DbType.DateTime;
				case "id":
					return DbType.Object;
				default:
					return DbType.Object;
			}
		}

		public static string GetDBType(this MemberInfo member)
		{
			switch (member.GetMemberType().Name)
			{
				case "String":
					return "text";
				case "Int32":
					return "integer";
				case "Double":
					return "double";
				case "Boolean":
					return "boolean";
				case "DateTime":
					return "date";
				case "id":
					return "integer";
				case "Thickness":
					return "text";
				default:
					return "text";
			}
		}

		/// <summary> Получает строку с флагами, заданными для атрибута, такими как "Primary Key" и "Not NULL" </summary>
		public static string GetFlags(this MemberInfo member)
		{
			string Result = string.Empty;
			Type memberType = member.GetMemberType();
			if (memberType == typeof(bool) || memberType == typeof(int) || memberType == typeof(double))
			{
				Result += " Not NULL";
				if (member.GetAttribute<DBAttributeAttribute>().IsPrimaryKey())
					Result += " Primary Key";
			}
			else if (memberType == typeof(id) || memberType == typeof(bool?))
			{
				Result = string.Empty;
			}
			else
			{
				Result = member.GetAttribute<DBAttributeAttribute>().GetFlags();
			}
			return Result;
		}

		/// <summary> Определяет, является ли данный тип класса типом TObject или наследуемым от него </summary>
		public static bool Is<TObject>(this Type classType)
		{
			Type type = classType;
			while (type != typeof(object) && type.IsClass)
			{
				if (type == typeof(TObject))
					return true;
				type = type.BaseType;
			}
			return false;
		}

		/// <summary> Находит объект TabItem коллекции ItemCollection, у которого свойство Header имеет значение header </summary>
		public static TabItem GetTabItem(this ItemCollection itemCollection, string header)
		{
			foreach (TabItem tabItem in itemCollection)
			{
				if (tabItem.Header.ToString() == header) return tabItem;
			}
			MessageBox.Show(
				string.Format("Объект TabItem, со значением Header='{0}', не найден!\n\nСейчас вывалится осключение!", header),
				"Неверное определение!",
				MessageBoxButton.OK,
				MessageBoxImage.Error);
			return null;
		}

		/// <summary> Преобразуем элементы массива в указанный тип </summary>
		public static object Cast(this IEnumerable array, Type resultType)
		{
			try
			{
				MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new Type[1] { resultType });
				object Result = castMethod.Invoke(null, new object[1] { array });
				MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(new Type[1] { resultType });
				return toArrayMethod.Invoke(null, new object[1] { Result });
			}
			catch
			{
				throw new ArgumentException("Неправильные входные данные!");
			}
		}

		/// <summary> Возвращает сущность данного типа </summary>
		public static DBEntity GetEntity(this Type type)
		{
			return new DBEntity(type);
		}

		/// <summary> Преобразует шестнадцатеричный код в цвет </summary>
		public static Color HexToColor(this string hex)
		{
			byte a = Convert.ToByte(hex.Substring(1, 2), 16);
			byte r = Convert.ToByte(hex.Substring(3, 2), 16);
			byte g = Convert.ToByte(hex.Substring(5, 2), 16);
			byte b = Convert.ToByte(hex.Substring(7, 2), 16);
			return Color.FromArgb(a, r, g, b);
		}

		/// <summary> Получает DataTemplate привязанного свойства, имеющего тип массив, для представления его в элементе управления System.Windows.Controls.ItemsControl </summary>
		public static DataTemplate GetDataTemplate(this DBEntity entity, PropertyInfo property)
		{
			if (!property.PropertyType.IsArray)
				throw new ArgumentException("Тип свойства должен быть массивом");
			
			return (DataTemplate)XamlReader.Parse(
				string.Format("<DataTemplate {0} {1} {2} {3} {4} > {5} </DataTemplate>",
					"xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'",
					"xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'",
					"xmlns:toolkit='clr-namespace:Xceed.Wpf.Toolkit;assembly=WPFToolkit.Extended'",
					"xmlns:converters='clr-namespace:ClassLibrary.Converters;assembly=ClassLibrary'",
					"xmlns:dataeditor='clr-namespace:DataEditor;assembly=DataEditor'",
					property.PropertyType.GetElementType().GetMethod("GetXAMLDataTemplate").Invoke(property.PropertyType.GetElementType().GetConstructor(new Type[0]).Invoke(new object[0]), null)
					));
		}

		/// <summary> Применяет описаные условия для коллекции элементов типа DBObject </summary>
		/// <param name="propertyName">Свойство, к которому приписаны условия</param>
		public static List<DBObject> ApplyConditions(this IEnumerable<DBObject> dbObjects, PropertyInfo property)
		{
			List<Func<DBObject,bool>> conditions = ConditionAttribute.GetConditions(property);
			foreach (Func<DBObject, bool> condition in conditions)
				dbObjects = dbObjects.Where(condition);
			return dbObjects.ToList();
		}

		/// <summary> Определяет, является ли данное число не положительным </summary>
		public static bool NotPositive(this int value)
		{
			return value <= 0;
		}

		/// <summary> Показывает, является ли данная сущность независимой </summary>
		public static bool IsIndependent(this DBEntity entity)
		{
			return entity.EntityType.GetAttribute<EntityAttribute>().IsIndependent;
		}

		/// <summary> Переводит строку </summary>
		public static string Translate(this string word)
		{
			string Result = word;
			if (string.IsNullOrWhiteSpace(Result)) return string.Empty;
			try
			{
				string text = new WebClient().DownloadString(string.Format("http://translate.yandex.net/api/v1/tr/translate?lang={0}&text={1}", "en-ru", word));
				Regex selectTranslatedWord = new Regex(@"<text>(?<result>.*?)</text>");
				return selectTranslatedWord.Match(text).Groups["result"].ToString();
			}
			catch
			{
				return word;
			}
		}

		/// <summary> Находит разность двух объектов типа DateTime </summary>
		/// <param name="subtrahend">Вычитаемое</param>
		public static DateTime Difference(this DateTime date, DateTime subtrahend)
		{
			return date.AddYears(-subtrahend.Year).AddMonths(-subtrahend.Month).AddDays(-subtrahend.Day);
		}

		/// <summary> Возвращает строку, содержащую информацию о возрасте человека </summary>
		public static string GetAgeString(this int demical)
		{
			string yearsString;
			int units = demical % 10;
			if (units == 1)
				yearsString = "год";
			if (units >= 2 && units <= 4 && !(demical >= 11 && demical <= 13))
				yearsString = "года";
			else
				yearsString = "лет";
			return string.Format("{0} {1}", demical, yearsString);
		}

		/// <summary> Умножает цвет на заданный компонент альфа </summary>
		public static Color Multiply(this Color color, double alfa)
		{
			Color Result = Color.Multiply((Color)color, (float)alfa);
			Result.A = 255;
			return Result;
		}

		/// <summary> Определяет индекс элемента в коллекции </summary>
		public static int IndexOf<T>(this IEnumerable<T> enumerable, T obj) where T : DBObject
		{
			for (int I = 0; I < enumerable.Count(); I++)
			{
				if (enumerable.ElementAt(I).ID == obj.ID)
					return I;
			}
			throw new Exception("Указанный оьъект не содержится в данной коллекции");
		}
	}
}