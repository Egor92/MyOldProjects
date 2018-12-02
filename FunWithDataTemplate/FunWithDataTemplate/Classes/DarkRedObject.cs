using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class DarkRedObject : RedObject
	{
		protected override string DefaultText
		{
			get
			{
				return "Dark Red";
			}
		}

		protected override Color ObjectColor
		{
			get
			{
				return Colors.DarkRed;
			}
		}
	}
}
