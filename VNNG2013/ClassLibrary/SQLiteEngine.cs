using System;
using System.Data.SQLite;
using System.Data;
using ClassLibrary.DBClass;

namespace ClassLibrary
{
	/// <summary>
	/// SQLite двигатель для удобного доступа к базе и выполнения запросов
	/// </summary>
	public class SQLiteEngine
	{
		protected SQLiteConnection connection;

		protected string dbFileName;

		/// <summary>
		/// Инцилизация двигателя
		/// </summary>
		public SQLiteEngine(string dbFileName)
		{
			this.dbFileName = dbFileName;

			//Инцилизация подключения к базе
			string connectionString = "Data Source=" + dbFileName;

			connection = new SQLiteConnection(connectionString);
		}

		/// <summary> Делает запрос, не требующего ответа </summary>
		/// <param name="sqlCommand">SQL-комманда</param>
		protected void Query(string sqlCommand)
		{
			using (SQLiteCommand command = new SQLiteCommand(sqlCommand, connection))
			{
				command.ExecuteNonQuery();
			}
		}

		/// <summary> Делает запрос на удаление </summary>
		/// <param name="TableName">Имя таблицы</param>
		/// <param name="ID">ИД удаляемого объекта</param>
		protected void Delete(string TableName, id ID)
		{
			string sqlCommand = string.Format("DELETE FROM '{0}' WHERE ID = '{1}'", TableName, ID.ToString());
			using (SQLiteCommand command = new SQLiteCommand(sqlCommand, connection))
			{
				command.ExecuteNonQuery();
			}
		}

		/// <summary> Делает запрос на добавление </summary>
		/// <param name="StringForInsert">Словарь значений</param>
		protected void Add(DBObject dbObject)
		{
			using (SQLiteCommand command = dbObject.CreateInsertCommand(connection))
			{
				command.ExecuteNonQuery();
			}
		}

		/// <summary> Выбрать таблицу </summary>
		/// <param name="TableName">Имя таблицы</param>
		/// <param name="Where">Условия</param>
		/// <returns>Таблица</returns>
		protected DataTable Select(string TableName, string Where)
		{
			DataTable table = new DataTable();
			table.TableName = TableName;
			string sqlCommand = string.Format("SELECT * FROM {0}", TableName);
			if (Where != string.Empty)
			{
				sqlCommand += " WHERE " + Where.ToString();
			}

			SQLiteCommand command = new SQLiteCommand(sqlCommand, connection);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			try
			{
				//заполняем таблицу
				adapter.Fill(table);
			}
			catch { }

			return table;
		}

		/// <summary> Выбрать таблицу </summary>
		/// <param name="TableName">Имя получаемой таблицы</param>
		/// <param name="AllTables">Дополнительные таблицы</param>
		/// <param name="Where">Условия</param>
		/// <returns>Таблица</returns>
		protected DataTable Select(string TableName, string AllTables, string Where)
		{
			DataTable table = new DataTable();
			table.TableName = TableName;
			string sqlCommand = string.Format("SELECT {0}.* FROM {1}", TableName, AllTables);
			if (Where != string.Empty)
			{
				sqlCommand += " WHERE " + Where.ToString();
			}

			SQLiteCommand command = new SQLiteCommand(sqlCommand, connection);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
			//заполняем таблицу
			adapter.Fill(table);

			return table;
		}

	}
}
