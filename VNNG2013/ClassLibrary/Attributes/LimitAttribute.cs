using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Attributes
{
	public enum IntegerLimit 
	{ 
		Rating, RatingLarge, PositiveOrZero, Positive
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class LimitAttribute : Attribute
	{
		public int LowLimit { get; set; }

		public int UpLimit { get; set; }

		public LimitAttribute()
		{
			this.LowLimit = Int32.MinValue;
			this.UpLimit = Int32.MaxValue;
		}

		public LimitAttribute(int upLimit)
		{
			this.LowLimit = 0;
			this.UpLimit = upLimit;
		}

		public LimitAttribute(int lowLimit, int upLimit)
		{
			this.LowLimit = lowLimit;
			this.UpLimit = upLimit;
		}

		public LimitAttribute(IntegerLimit limitType)
		{
			switch (limitType)
			{
				case IntegerLimit.Rating:
					this.LowLimit = -2;
					this.UpLimit = 10;
					break;
				case IntegerLimit.RatingLarge:
					this.LowLimit = -2;
					this.UpLimit = 100;
					break;
				case IntegerLimit.PositiveOrZero:
					this.LowLimit = 0;
					this.UpLimit = Int32.MaxValue;
					break;
				case IntegerLimit.Positive:
					this.LowLimit = 1;
					this.UpLimit = Int32.MaxValue;
					break;
			}
		}
	}
}
