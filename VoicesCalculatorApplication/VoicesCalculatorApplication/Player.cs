using System;

namespace VoicesCalculatorApplication
{
	public class Player : IComparable<Player>
	{
		public string Name { get; set; }

		public string Surname { get; set; }


        public Player(string name, string surname)
		{
			Name = name;
			Surname = surname;
		}

	    public int CompareTo(Player other)
	    {
	        if (other == null)
	            return -1;
	        var nameComparation = string.CompareOrdinal(Name, other.Name);
	        if (nameComparation != 0)
	            return nameComparation;
            var surnameComparation = string.CompareOrdinal(Surname, other.Surname);
            if (surnameComparation != 0)
                return surnameComparation;
            return 0;
	    }

	    public override string ToString()
		{
		    var format = string.IsNullOrWhiteSpace(Name)
		        ? "{1}"
		        : "{0} {1}";
            return string.Format(format, Name, Surname);
		}
	}
}
