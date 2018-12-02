using System;
using System.Collections.Generic;
using System.Linq;

namespace FindTheExcessImage
{
    public class Word
    {
        public string Original { get; set; }

        public string Translated { get; set; }

		public Word(string original, string translated)
		{
            Original = original;
            Translated = translated;
		}


		public override string ToString()
		{			
			return Original;
		}
    }
}
