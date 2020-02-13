using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.Attributes;
using ClassLibrary.DBClass;
using System.Windows;
using System.Data.SQLite;

namespace ClassLibrary
{
	public class SQLiteAction : SQLiteEngine
	{
		private SQLiteTransaction transaction;

		public SQLiteAction(string dbFileName) : base(dbFileName) { }

		public string GetDBFileName()
		{
			return this.dbFileName;
		}

		public void OpenDBConnection()
		{
			base.connection.Open();
			//this.transaction = this.connection.BeginTransaction();
		}

		public void CloseDBConnection()
		{
			/*this.transaction.Commit();
			this.transaction.Dispose();*/
			base.connection.Close();
		}

		/// <summary> Получает объект по его ИД </summary>
		/// <param name="entity">Сущность</param>
		/// <param name="ID">ИД</param>
		/// <returns>Объект</returns>
		public DBObject GetObject(DBEntity entity, int ID)
		{
			DataTable table = base.Select(
				entity.Name,
				"ID=" + ID.ToString()
				);

			if (table.Rows.Count > 0)
			{
				return DBObject.GetDBObject(entity, table.Rows[0]);
			}
			else
			{
				throw new ArgumentOutOfRangeException(entity.TranslationName + " ID");
			}
		}

		/// <summary> Получает все объекты из таблицы, отвечающие заданным условиям </summary>
		/// <param name="entity">Сущность</param>
		/// <param name="Where">Условия</param>
		/// <returns>Массив объектов</returns>
		public DBObject[] GetObjects(DBEntity entity, string Where = "")
		{
			DataTable table;
			string TablesNames = entity.Name;

			table = base.Select(TablesNames, Where);

			DBObject[] Result = new DBObject[table.Rows.Count];

			for (int I = 0; I < table.Rows.Count; I++)
			{
				Result[I] = DBObject.GetDBObject(entity, table.Rows[I]);
			}
			return Result;
		}

		/// <summary> Добавляет объект в БД </summary>
		/// <param name="dbObject">Объект</param>
		public void AddObject(DBObject dbObject)
		{
			base.Add(dbObject);
		}

		/// <summary> Удаляет объект </summary>
		/// <param name="dbObject">Объект</param>
		public void DeleteObject(DBObject dbObject)
		{
			base.Delete(dbObject.GetType().Name, dbObject.ID);
		}

		/// <summary> Создаёт таблицу для класса в Базе Данных </summary>
		/// <param name="entity">Сущность, для которой необходимо создать таблицу</param>
		public void CreateTable(DBEntity entity)
		{
			StringBuilder stringBuilder = new StringBuilder();

			//Берём все свойства, помеченные DBAttribute
			foreach (MemberInfo member in entity.EntityType.GetMembers<DBAttributeAttribute>())
			{
				stringBuilder.Append(
					string.Format("{0} {1} {2}, ",
							//Имя атрибута
							member.Name,
							//Тип данных
							member.GetDBType(),
							//Флаги атрибута, такие как "Primary Key", "Unique", "Not NULL" и "Autoincrement"
							member.GetFlags()
						)
					);
			}

			//Удаляем запятую в конце строки
			stringBuilder.Remove(stringBuilder.Length - 2, 2);

			base.Query(
				string.Format("Create Table {0} ({1})",
					entity.Name,
					stringBuilder.ToString()
					)
				);
		}
	}
}
