using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
	public class id : IComparable
	{
		private Nullable<int> value;

		public int Value
		{
			get
			{
				return this.value.Value;
			}
			set
			{
				this.value = value;
			}
		}

		public bool HasValue
		{
			get
			{
				return this.value.HasValue;
			}
		}

		public bool IsNull
		{
			get
			{
				return !this.value.HasValue;
			}
		}

		public id(Nullable<int> id = null)
		{
			this.value = id;
		}

		public static bool operator==(id id, id other)
		{
			return (id.HasValue && other.HasValue && id.value == other.Value);
		}

		public static bool operator!=(id id, id other)
		{
			return !(id == other);
		}

		public static bool operator==(id id, int value)
		{
			return (id.HasValue && id.value == value);
		}

		public static bool operator!=(id id, int value)
		{
			return !(id == value);
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			if (this.HasValue)
				return this.Value.ToString();
			else
				return string.Empty;
		}

		public int CompareTo(object obj)
		{
			return this.Value.CompareTo(((id)obj).Value);
		}

		public static implicit operator id(int value)
		{
			return new id(value);
		}
	}
}
