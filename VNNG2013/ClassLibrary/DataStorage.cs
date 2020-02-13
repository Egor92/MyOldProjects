using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.Attributes;
using ClassLibrary.DBClass;
using System.IO;
using System.Windows;
using System.Data.SQLite;

namespace ClassLibrary
{
	public class DataStorage
	{
		private SQLiteAction action;

		private Dictionary<DBEntity, List<DBObject>> Storage { get; set; }

		public Dictionary<DBEntity, List<DBObject>> Cache { get; set; }

		/// <summary> Возвращает текущий день в игре </summary>
		public DateTime CurrentDate
		{
			get
			{
				GameData gameData = (GameData)this.Storage[typeof(GameData)].SingleOrDefault();
				if (gameData != null)
					return gameData.CurrentDate;
				else
					return new DateTime(2012, 8, 1);
			}
		}

		
		/// <summary> Создаёт новый экземпляр типа Хранилище Данных </summary>
		/// <param name="dbFileName">Путь к Базе Данных</param>
		/// <param name="ToContainsCache">Необходимо ли содержать пользовательский кэш</param>
		public DataStorage(string dbFileName, bool ToContainsCache = false)
		{
			this.action = new SQLiteAction(dbFileName);
			this.Storage = new Dictionary<DBEntity, List<DBObject>>();
			if (ToContainsCache)
				this.Cache = new Dictionary<DBEntity, List<DBObject>>();
			this.BuildStorage(ToContainsCache);

			//Изымаем данные из БД, записываем их в Хранилище...
			this.action.OpenDBConnection();
			this.GetAllTablesFromDataBase();
			this.action.CloseDBConnection();
		}
		
		/// <summary>
		/// Строит структуру "хранилища данных" на основе классов, помеченных атрибутом EntityAttribute
		/// Для каждого класса, помеченного атрибутом EntityAttribute, создаётся список, в котором будут храниться экземпляры данного класса
		/// </summary>
		private void BuildStorage(bool ToContainsCache)
		{
			foreach (DBEntity entity in DBEntity.GetEntities())
			{
				this.Storage.Add(entity, new List<DBObject>());
				if (ToContainsCache)
				{
					EntityAttribute entityAttribute = entity.EntityType.GetAttribute<EntityAttribute>() as EntityAttribute;
					if (entityAttribute.IsIndependent)
						this.Cache.Add(entity, new List<DBObject>());
				}
			}
		}

		private void GetAllTablesFromDataBase()
		{
			foreach (DBEntity entity in this.Storage.Keys)
			{
				DBObject[] dbObjects = this.action.GetObjects(entity);
				foreach (DBObject dbObject in dbObjects)
				{
					this.AddDBObject(dbObject);
				}
			}
		}

		/// <summary> Создаёт новый объект и сохраняет его в Хранилище Данных </summary>
		public TObject CreateDBObject<TObject>() where TObject : DBObject
		{
			id ID = this.GetID(typeof(TObject));
			TObject Result = DBObject.GetEmpty(typeof(TObject), ID) as TObject;
			this.AddDBObject(Result);
			return Result;
		}

		/// <summary> Создаёт новый объект и сохраняет его в Хранилище Данных </summary>
		public DBObject CreateDBObject(DBEntity entity)
		{
			id ID = this.GetID(entity);
			DBObject Result = DBObject.GetEmpty(entity, ID);
			this.AddDBObject(Result);
			return Result;
		}

		/// <summary> Добавляет объект в Хранилище Данных </summary>
		public void AddDBObject(DBObject dbObject)
		{
			if (dbObject.ID.IsNull) dbObject.SetID(this.GetID(dbObject.GetEntity()));
			dbObject.DataStorage = this;
			(this.Storage.Single(x => x.Key == dbObject.GetEntity()).Value as List<DBObject>).Add(dbObject);
		}

		/// <summary> Находит объект в Хранилище Данных </summary>
		public TObject GetDBObject<TObject>(int ID) where TObject : DBObject
		{
			return this.GetDBObject(new DBEntity(typeof(TObject)), ID) as TObject;
		}

		/// <summary> Находит объект в Хранилище Данных </summary>
		public DBObject GetDBObject(DBEntity entity, int ID)
		{
			IEnumerable<DBObject> enumerable = (this.Storage[entity] as List<DBObject>).Where(x => x.ID == ID);
			if (enumerable.Count() == 0)
			{
				MessageBox.Show(
					string.Format("Был запрошен несуществующий объект типа {0} с ID={1}", entity.TranslationName, ID),
					"Внимание!",
					MessageBoxButton.OK,
					MessageBoxImage.Warning);
				return null;
			}
			else
			{
				DBObject Result = enumerable.First();
				return Result;
			}
		}

		/// <summary> Возвращает случайный элемент </summary>
		public DBObject GetRandomObject(DBEntity entity)
		{
			IEnumerable<DBObject> enumerable = this.Storage[entity];
			return enumerable.ElementAt(new Random().Next(0, enumerable.Count() - 1));
		}

		/// <summary> Удаляет объект из Хранилища Данных </summary>
		public bool DeleteDBObject(DBObject dbObject)
		{
			dbObject.DeleteLinks();
			return this.Storage[dbObject.GetType().GetEntity()].Remove(dbObject);
		}

		public List<T> GetDBObjects<T>() where T : DBObject
		{
			List<T> Result = this.Storage.Single(x => x.Key == typeof(T).GetEntity()).Value.Cast<T>().ToList();
			return Result;
		}

		public List<DBObject> GetDBObjects(DBEntity entity)
		{
			List<DBObject> Result = this.Storage.Single(x => x.Key == entity).Value;
			return Result;
		}

		public List<DBEntity> GetEntities()
		{
			return this.Storage.Keys.ToList();
		}





		#region Методы для копирования данных
		/// <summary> Копирует данные из одной БД в другую </summary>
		/// <param name="toFile">Файл, в который копируються данные</param>
		public void Copy(string toFile)
		{
			this.action = new SQLiteAction(toFile);
			this.SendAllTablesToDataBase(toFile);
		}

		private void SendAllTablesToDataBase(string toFile)
		{
			foreach (KeyValuePair<DBEntity, List<DBObject>> keyValuePair in this.Storage)
			{
				this.SendTableToDataBase(keyValuePair.Value);
			}
		}

		private void SendTableToDataBase(List<DBObject> objectsList)
		{
			foreach (DBObject dbObject in objectsList)
			{
				this.action.AddObject(dbObject);
			}
		}
		#endregion





		#region Методы для сохранения
		/// <summary> Сохранить данные из Хранилища в БД </summary>
		public void Save()
		{
			string lastPath = this.action.GetDBFileName();
			string directory = SpecialPaths.DataBases;
			string tmpDirectory = string.Format("{0}\\tmp", directory);
			if (!Directory.Exists(tmpDirectory)) Directory.CreateDirectory(tmpDirectory);
			string file = Path.GetFileNameWithoutExtension(lastPath);
			string extension = Path.GetExtension(lastPath);
			string tmpPath = string.Format("{0}\\tmp\\{1} (tmp){2}", directory, file, extension);
			File.Delete(tmpPath);
			//Во избежания потери данных в случае ошибок, старая БД временно существует под именем с пометкой (copy)
			File.Move(lastPath, tmpPath);
			this.SaveData();
		}

		/// <summary> Сохранить данные из Хранилища в БД </summary>
		public void SaveAs(string fileName)
		{
			File.Delete(fileName);
			this.action = new SQLiteAction(fileName);
			this.SaveData();
		}

		/// <summary> Создаёт таблицы в БД и сохраняет все данные их Хранилища </summary>
		private void SaveData()
		{
			this.action.OpenDBConnection();
			this.CreateTables();
			foreach (KeyValuePair<DBEntity, List<DBObject>> keyValuePair in this.Storage)
			{
				foreach (DBObject dbObject in keyValuePair.Value)
				{
					dbObject.Save(this.action);
				}
			}
			this.action.CloseDBConnection();
		}

		/// <summary> Создаёт таблицы для всех классов, помеченных атрибутом EntityAttribute </summary>
		private void CreateTables()
		{
			foreach (DBEntity entity in DBEntity.GetEntities())
			{
				action.CreateTable(entity);
			}
		}
		#endregion





		#region Методы для работы с кэшем
		public void AddToCache(DBObject dbObject)
		{
			List<DBObject> list = this.Cache[dbObject.GetEntity()] as List<DBObject>;
			list.Remove(dbObject);
			list.Insert(0, dbObject);
			//Очистка кэша
			if (list.Count > 50)
				this.Cache[dbObject.GetEntity()] = list.Take(20).ToList();
		}

		public List<DBObject> GetFromCache(DBEntity entity, int count = 10)
		{
			List<DBObject> list = this.Cache[entity] as List<DBObject>;
			return list.Distinct().Take(count).ToList();
		}
		#endregion






		/// <summary> Возвращает свободный идентификатор для данного типа объекта </summary>
		/// <param name="entityType">Тип объекта</param>
		/// <returns>Идентификатор</returns>
		public id GetID(DBEntity entity)
		{
			id Result = null;
			List<DBObject> objects = this.Storage.First(x => x.Key == entity).Value;
			if (objects.Count == 0)
			{
				Result = new id(1);
			}
			else
			{
				objects = objects.OrderBy(x => x.ID).ToList();
				bool isFinded = false;
				for (int I = 0; I < objects.Count; I++)
				{
					if (objects[I].ID != I + 1)
					{
						Result = new id(I + 1);
						isFinded = true;
						break;
					}
				}
				if (!isFinded) Result = objects.Last().ID.Value + 1;
			}
			return Result;
		}

		/// <summary> Создаёт экземпляр GameData для новой игры </summary>
		public void CreateGameData()
		{
			if (this.Storage[typeof(GameData)].Count > 0)
				throw new Exception("Уже имеется экземпляр GameData");
			this.CreateDBObject(typeof(GameData));
		}


	}
}
