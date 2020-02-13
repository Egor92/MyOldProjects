using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using ClassLibrary.Attributes;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Media;
using System.Windows.Markup;
using System.Collections;

namespace ClassLibrary.DBClass
{
	public abstract class DBObject : INotifyPropertyChanged
	{
		[DBAttribute(false, true)]
		[EditingProperty("Идентификатор")]
		public id ID { get; protected set; }

		public DataStorage DataStorage { get; set; }
		
		/// <summary> Возвращает строковое представление данного объекта </summary>
		public virtual string DisplayedText
		{
			get { return base.ToString(); }
		}

		public DBObject() 
		{
			//Каждому свойству, имеющему тип 'id', задаём значение null
			IEnumerable<MemberInfo> members = this.GetType().GetMembers<DBAttributeAttribute>().Where(x => x.GetMemberType() == typeof(id));
			foreach (MemberInfo member in members)
			{
				member.SetValue(this, new id());
			}
		}

		public DBObject(DataRow dataRow)
		{
			this.InitializeDBObject(dataRow);
		}

		public override string ToString()
		{
			return this.DisplayedText;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void ToNotifyChanges(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary> Устанавливает новое значение свойству ID, если оно равно null </summary>
		public void SetID(id newID)
		{
			if (this.ID.IsNull)
				if (newID.HasValue)
					this.ID = newID;
				else
					throw new ArgumentNullException("newID = null");
			else
				throw new Exception("Нельзя присваивать значение свойству ID, если оно не является null");
		}
	
		/// <summary> Содаёт объект-"пустышку" </summary>
		public static DBObject GetEmpty(DBEntity entity, id ID)
		{
			//Создаём объект
			DBObject Result = (DBObject)entity.EntityType.GetConstructor(new Type[0]).Invoke(new object[0]);
			//Задаём инициализацию по-умолчанию для нового объекта
			Result.EmptyInitialization();
			//Устанавливаем заданный идентификатор
			Result.ID = ID;
			return Result;
		}

		/// <summary> Задаёт инициализацию по-умолчанию для новых объектов </summary>
		protected virtual void EmptyInitialization()
		{
			foreach (PropertyInfo property in this.GetType().GetProperties().Where(x => x.PropertyType == typeof(string)))
			{
				try
				{
					property.SetValue(this, string.Empty, null);
				}
				catch { }
			}
		}

		/// <summary> Возвращает SQL-комманду данного Dictionary для вставки </summary>
		public SQLiteCommand CreateInsertCommand(SQLiteConnection connection)
		{
			SQLiteCommand command = new SQLiteCommand(connection);
			StringBuilder[] Result = new StringBuilder[] { new StringBuilder(), new StringBuilder() };

			int I = 0;
			MemberInfo[] members = this.GetType()
				.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
				.Where(x => x.HasAttribute<DBAttributeAttribute>() && (x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property)).ToArray();
			foreach(MemberInfo member in members)
			{
				SQLiteParameter param = new SQLiteParameter();
				param.ParameterName = "@p" + I;

				string key = member.Name;
				object value = member.GetValue(this);

				if (value == null)
				{
					param.DbType = DbType.Object;
					param.Value = null;
				}
				else
				{
					param.DbType = value.GetType().GetDbType();
					if (value.GetType().Is<id>())
					{
						if ((value as id).HasValue)
							param.Value = (value as id).Value;
						else
							param.Value = null;
					}
					else
					{
						param.Value = value;
					}
				}


				Result[0].AppendFormat("{0}, ", key);
				Result[1].AppendFormat("{0}, ", param.ParameterName);

				command.Parameters.Add(param);
				I++;
			}

			//Вырезаем ", "
			Result[0].Remove(Result[0].Length - 2, 2);
			Result[1].Remove(Result[1].Length - 2, 2);

			command.CommandText = string.Format("INSERT INTO {0} ({1}) values ({2})",
					this.GetType().Name,
					Result[0],
					Result[1]
					);

			return command;
		}

		/// <summary> Создаёт новый объект, инициализируя его значениями из словаря </summary>
		public static DBObject GetDBObject(DBEntity entity, DataRow dataRow)
		{
			DBObject Result = (DBObject)Type.GetType(string.Format("ClassLibrary.DBClass.{0}", entity.Name)).GetConstructor(new Type[0]).Invoke(new object[0]);
			Result.InitializeDBObject(dataRow);
			return Result;
		}

		/// <summary> Получает объект DBEntity, соответствующий типу данного объекта </summary>
		public DBEntity GetEntity()
		{
			return new DBEntity(this.GetType());
		}

		/// <summary> Инициализирует объект значениями из DataRow </summary>
		private void InitializeDBObject(DataRow dataRow)
		{
			List<MemberInfo> members = this.GetType().GetMembers<DBAttributeAttribute>();
			foreach (MemberInfo member in members)
			{
				object value = null;

				//Если в последней сборке ClassLibrary было создано новое свойство, то его ещё не будет в БД.
				//Устанавливать для него значение не нужно, так как его ещё просто нет.
				//Для всех остальных "нормальных" случаев нужно делать так:
				if (dataRow.Table.Columns.Contains(member.Name))
				{
					value = dataRow[member.Name];

					Type memberType = member.GetMemberType();
					if (value is DBNull)
					{
						if (memberType == typeof(id))
							member.SetValue(this, new id(null));
						else
							member.SetValue(this, null);
					}
					else
					{
						if (memberType == typeof(string))
						{
							if (value != null)
								member.SetValue(this, (value as string).Trim());
							else
								member.SetValue(this, string.Empty);
						}
						else if (memberType == typeof(int))
						{
							member.SetValue(this, System.Convert.ToInt32(value));
						}
						else if (memberType == typeof(id))
						{
							int idValue;
							if (int.TryParse(value.ToString(), out idValue))
								member.SetValue(this, new id(idValue));
							else
								member.SetValue(this, new id(null));
						}
						else if (memberType == typeof(bool))
						{
							member.SetValue(this, bool.Parse(value.ToString()));
						}
						else if (memberType == typeof(bool?))
						{
							if (value  != null)
							{
								//false
								if (value.ToString() == "0")								
									member.SetValue(this, false);
								//true
								else						
									member.SetValue(this, true);
							}
							//null
							else
								member.SetValue(this, null);
						}
						else if (memberType == typeof(double))
						{
							member.SetValue(this, System.Convert.ToDouble(value));
						}
						else if (memberType == typeof(DateTime))
						{
							DateTime dateTime;
							if (DateTime.TryParse(value.ToString(), out dateTime))
								member.SetValue(this, dateTime);
							else
								member.SetValue(this, null);
						}
						else if (memberType == typeof(Color))
						{
							if (value != null)
								member.SetValue(this, new ColorConverter().ConvertFrom((string)value));
							else
								member.SetValue(this, Colors.Black);
						}
						else if (memberType == typeof(Thickness))
						{
							if (value != null)
							{
								double[] intValues = ((string)value).Split(',').Take(2).Select(x => Convert.ToDouble(x)).ToArray();
								member.SetValue(this, new Thickness(intValues[0], intValues[1], 0.0, 0.0));
							}
							else
								member.SetValue(this, new Thickness());
						}
						else if (memberType.IsEnum)
						{
							member.SetValue(this, System.Convert.ToInt32(value));
						}
						else
						{
							member.SetValue(this, value);
						}
					}
				}
			}
		}

		/// <summary> Сохраняет или добавляет обьект в базу данных </summary>
		public void Save(SQLiteAction action)
		{
			action.AddObject(this);
		}

		/// <summary> Удаляет ссылки на себя со всех объектов Хранилища Данных </summary>
		public void DeleteLinks()
		{
			Action<DBObject,PropertyInfo> action;
			List<DBEntity> allEntities = this.DataStorage.GetEntities();
			foreach (DBEntity entity in allEntities)
			{
				if (entity.IsIndependent())
					action = (dbObject, property) => property.SetValue(dbObject, null, null);
				else
					action = (dbObject, property) => this.DataStorage.DeleteDBObject(dbObject);

				IEnumerable<PropertyInfo> properties = entity
														.EntityType
														.GetProperties(BindingFlags.Public | BindingFlags.Instance)
														.Where(x => x.PropertyType == this.GetType());
				if (properties != null)
				{
					foreach (PropertyInfo property in properties)
					{
						foreach (DBObject dbObject in this.DataStorage.GetDBObjects(entity).ToArray())
						{
							DBObject propertyValue = property.GetValue(dbObject, null) as DBObject;
							if (propertyValue != null)
							{
								if (propertyValue.ID == this.ID)
								{
									action.Invoke(dbObject, property);
								}
							}
						}
					}
				}
			}
		}

		/// <summary> Определяет, содержит ли строковое представление объекта данную строку </summary>
		public bool Contains(string str)
		{
			return (this.DisplayedText.ToLower().IndexOf(str.ToLower()) > -1);
		}







		/// <summary> Переопределите содержимое DataTemplate для правильного отображения данных в объектах ItemsCollection </summary>
		public virtual string GetXAMLDataTemplate()
		{
			return "<TextBlock Text='{Binding}' />";
		}














		/// <summary> Возвращает соответствующее свойству с именем "propertyName" свойство, имеющее тип id </summary>
		private FieldInfo GetIDField(string propertyName)
		{
			FieldInfo idField = this.GetType().GetField(string.Format("{0}ID", propertyName), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			if (idField == null)
				throw new Exception(
					string.Format("У объекта типа '{0}' для свойства '{1}' не найдено соответствующее ему поле '{2}'",
					this.GetType().Name,
					propertyName,
					string.Format("{0}ID", propertyName))
					);

			if (idField.FieldType.Is<id>())
				return idField;
			else
				throw new Exception(string.Format("Поле '{0}' должно иметь тип 'id'", string.Format("{0}ID", propertyName)));
		}

		/// <summary> Возвращает соответствующее свойству с именем "propertyName" поле, содержащее значение </summary>
		private FieldInfo GetFieldForProperty(string propertyName)
		{
			string fieldName = string.Format("{0}{1}", char.ToLower(propertyName.First()), new string(propertyName.Skip(1).ToArray()));
			FieldInfo fieldInfo = this.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

			if (fieldInfo == null)
				throw new Exception(
					string.Format("Для свойства '{0}' не найдено соответствующее ему поле '{1}'",
					propertyName,
					fieldName
					));

			return fieldInfo;
		}


		/// <summary> Метод "Get" для свойств, имеющих тип DBObject </summary>
		protected T GetDBObjectPropertyValue<T>(string propertyName) where T : DBObject
		{
			FieldInfo idField = this.GetIDField(propertyName);
			//id объекта
			id _id = idField.GetValue(this) as id;
			//Поле, содержащее значение
			FieldInfo field = this.GetFieldForProperty(propertyName);
			//Объект, хранящийся в поле (если мы его ещё не вызывали, то он равен null)
			T dbObject = field.GetValue(this) as T;
			//Если объект, хранящийся в dependencyProperty, ещё не вызывали и в id хранится значение, то вызовем этот объект
			if (dbObject == null && _id.HasValue)
			{
				//Определяем тип объекта
				Type type = typeof(T);
				//Находим объект
				T value = (T) this.DataStorage.GetDBObject(new DBEntity(type), _id.Value);
				//Сохраняем его в поле
				field.SetValue(this, value);
				return value;
			}
			return dbObject;
		}

		/// <summary> Метод "Set" для свойств, имеющих тип DBObject </summary>
		protected void SetDBObjectPropertyValue(string propertyName, DBObject value)
		{
			FieldInfo idField = this.GetIDField(propertyName);
			if (value == null)
				idField.SetValue(this, new id(null));
			else
				idField.SetValue(this, value.ID);
			//Поле, содержащее значение
			FieldInfo field = this.GetFieldForProperty(propertyName);
			field.SetValue(this, value);
			this.ToNotifyChanges(propertyName);
		}


		/// <summary> Метод "Get" для свойств, имеющих тип DBObject, которые содержут объект, ссылающийся на данный объект </summary>
		protected T GetBindedPropertyValue<T>(string propertyName, string linkedObjectPropertyName = null) where T : DBObject
		{
			if (linkedObjectPropertyName == null) linkedObjectPropertyName = this.GetType().Name;
			PropertyInfo thisPropertyInfo = this.GetType().GetProperty(propertyName);
			FieldInfo linkedObjectIDField = typeof(T).GetField(linkedObjectPropertyName + "ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (linkedObjectIDField == null)
				throw new Exception(string.Format("Для объекта типа {0} не найдено указанное свойство с именем {1}ID", typeof(T).Name, linkedObjectPropertyName));
			IEnumerable<DBObject> dbObjects = this.DataStorage.GetDBObjects(typeof(T).GetEntity()).ApplyConditions(thisPropertyInfo);
			DBObject Result = dbObjects.SingleOrDefault(x => linkedObjectIDField.GetValue(x) as id == this.ID);
			return Result as T;
		}

		/// <summary> Метод "Set" для свойств, имеющих тип DBObject, которые содержут объект, ссылающийся на данный объект </summary>
		protected void SetBindedPropertyValue(string propertyName, DBObject value, string linkedObjectPropertyName = null)
		{
			//Имя свойства объекта 'value', значение которого нужно поменять
			if (linkedObjectPropertyName == null) linkedObjectPropertyName = this.GetType().Name;
			PropertyInfo thisPropertyInfo = this.GetType().GetProperty(propertyName);
			Type valueType = thisPropertyInfo.PropertyType;
			//Свойство объекта 'value', значение которого нужно поменять
			PropertyInfo linkedObjectProperty = valueType.GetProperty(linkedObjectPropertyName);
			//Зануляем объект типа valueType, значения свойтсва которого ссылаются на ДАННЫЙ объект
			DBObject dbObject = thisPropertyInfo.GetValue(this, null) as DBObject;
			if (dbObject != null)
				linkedObjectProperty.SetValue(dbObject, null, null);
			//Задаём значение
			if (value != null)
				linkedObjectProperty.SetValue(value, this, null);
			this.ToNotifyChanges(propertyName);
		}


		/// <summary> Метод "Get" для свойств, имеющих тип DBObject[], которые содержут объекты, ссылающиеся на данный объект </summary>
		protected T[] GetBindedArrayPropertyValue<T>(string propertyName, string linkedObjectPropertyName = null) where T : DBObject
		{
			//Имя свойства объекта, массив которых нужно получить
			if (linkedObjectPropertyName == null) linkedObjectPropertyName = this.GetType().Name;
			//Свойство, содержащее ID объекта
			string linkedObjectIDFieldName = string.Format("{0}ID", linkedObjectPropertyName);
			//Свойство, вызовшее данный метод
			PropertyInfo thisPropertyInfo = this.GetType().GetProperty(propertyName);
			//Свойство объекта, массив которых нужно получить
			FieldInfo arrayObjectsIDFieldInfo = typeof(T).GetField(linkedObjectIDFieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			//Если свойство не найдено, значит класс определён неверно...
			if (arrayObjectsIDFieldInfo == null)
				//...выбрасываем исключение
				throw new Exception(string.Format("Неверное определение класса {0}: отсутствует запрошенное свойство {1}", typeof(T).Name, linkedObjectPropertyName));
			T[] arrayObjects = this.DataStorage
				.GetDBObjects(typeof(T))
				.Where(x => arrayObjectsIDFieldInfo.GetValue(x) as id == this.ID)
				//Применяем фильтрацию по указанному в описании класса условию
				.ApplyConditions(thisPropertyInfo)
				.Cast<T>()
				.ToArray();
			//Применяем сортировку по указанному в описании класса свойству
			SortingAttribute sortingAttribute = thisPropertyInfo.GetAttribute<SortingAttribute>();
			if (sortingAttribute != null)
			{
				MemberInfo sortingMemberInfo = sortingAttribute.GetMemberInfo(typeof(T));
				if (sortingMemberInfo is PropertyInfo)
					arrayObjects = arrayObjects.OrderBy(x => (sortingMemberInfo as PropertyInfo).GetValue(x, null)).ToArray();
				else if (sortingMemberInfo is MethodInfo)
					arrayObjects = arrayObjects.OrderBy(x => (sortingMemberInfo as MethodInfo).Invoke(x, null)).ToArray();
			}
			//Результат
			return arrayObjects;
		}

		/// <summary> Метод "Set" для свойств, имеющих тип DBObject[], которые содержут объекты, ссылающиеся на данный объект </summary>
		protected void SetBindedArrayPropertyValue<T>(string propertyName, T[] value, string linkedObjectPropertyName = null) where T : DBObject
		{
			value = value.Distinct().ToArray();
			//Свойтсво объекта, вызвавшего данный метод
			PropertyInfo thisPropertyInfo = this.GetType().GetProperty(propertyName);
			if (linkedObjectPropertyName == null) linkedObjectPropertyName = this.GetType().Name;
			//Свойство элементов массива, содержащих значение, ссылающееся на объект, вызвавший метод SetBindedArrayPropertyValue
			PropertyInfo valuePropertyInfo = typeof(T).GetProperty(linkedObjectPropertyName);
			//Старый список объектов
			DBObject[] dbObjects = thisPropertyInfo.GetValue(this, null) as DBObject[];
			//Зануляем свойства объектов старого списка
			if (dbObjects != null)
				foreach (DBObject dbObject in dbObjects.Except(value))
					valuePropertyInfo.SetValue(dbObject, null, null);
			//Задаём значение свойства объектов нового списка
			foreach (DBObject dbObject in value)
				if (dbObject != null)
					valuePropertyInfo.SetValue(dbObject, this, null);

			this.ToNotifyChanges(propertyName);
		}


		/// <summary> Метод "Get" для свойств, имеющих тип DBObject[], которые содержут объекты, ссылающиеся на данный объект через связь "Многие-ко-многим" </summary>
		protected T[] GetBindedArrayPropertyValue<T>(DBEntity manyToManyEntity) where T : DBObject
		{
			EntityAttribute entityAttribute = manyToManyEntity.EntityType.GetAttribute<EntityAttribute>();
			if (!entityAttribute.HasTypes(this.GetType(), typeof(T)))
				throw new ArgumentException("Неверно указны обобщенный параметр или manyToManyEntity");
			PropertyInfo thisTypeProperty = manyToManyEntity.EntityType.GetProperty(this.GetType().Name);
			PropertyInfo otherTypeProperty = manyToManyEntity.EntityType.GetProperty(typeof(T).Name);
			return this.DataStorage.GetDBObjects(manyToManyEntity)
				.Where(x => thisTypeProperty.GetValue(x, null) == this)
				.Select(x => otherTypeProperty.GetValue(x, null)).Cast<T>().ToArray();
		}

	}
}