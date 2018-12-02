using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class OrangeRedObject : RedObject
	{
		protected override string DefaultText
		{
			get
			{
				return "Orange Red";
			}
		}

		protected override Color ObjectColor
		{
			get
			{
				return Colors.OrangeRed;
			}
		}
	}
}
