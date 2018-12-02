using System;
using System.IO;
using System.Linq;

namespace VoicesCalculatorApplication
{
    public class Voice
	{
        #region Fileds

        public const int PlacesCount = 10;

        #endregion

        #region Places

        public Player[] Places { get; private set; }

        #endregion

        public Voice()
        {
            Places = new Player[10];
        }
	}
}
