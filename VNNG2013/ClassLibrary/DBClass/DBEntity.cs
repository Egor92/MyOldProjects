using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ClassLibrary.Attributes;

namespace ClassLibrary.DBClass
{
	public struct DBEntity
	{
		private Type entityType;
		public Type EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		public string Name
		{
			get
			{
				return this.EntityType.Name;
			}
		}

		public string TranslationName
		{
			get
			{
				return (this.EntityType.GetAttribute<EntityAttribute>() as EntityAttribute).TranslationName;
			}
		}

		public DBEntity(Type entityType)
		{
			if (entityType != null)
				if (entityType.Is<DBObject>())
				{
					this.entityType = entityType;
				}
				else
					throw new ArgumentException("Аргумент entityType не является DBObject");
			else
				throw new ArgumentNullException("Аргумент entityType является null");
		}

		public override string ToString()
		{
			return this.TranslationName;
		}

		/// <summary>  Возвращает все сущности, реализованные в сборку ClassLibrary </summary>
		public static List<DBEntity> GetEntities()
		{
			return Assembly.GetExecutingAssembly().GetTypes()
				.Where(x => x.Is<DBObject>() && x.GetCustomAttributes(typeof(EntityAttribute), false).Length > 0)
				.Select(x => x.GetEntity()).ToList();
		}

		public static bool operator == (DBEntity entity1, DBEntity entity2)
		{
			return entity1.EntityType == entity2.EntityType;
		}

		public static bool operator != (DBEntity entity1, DBEntity entity2)
		{
			return entity1.EntityType != entity2.EntityType;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator DBEntity(Type value)
		{
			return new DBEntity(value);
		}
	}
}
