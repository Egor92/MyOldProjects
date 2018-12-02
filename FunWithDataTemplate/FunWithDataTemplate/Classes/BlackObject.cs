using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class BlackObject : BaseObject
	{
		protected override string DefaultText
		{
			get { return "Black"; }
		}

		protected override System.Windows.Media.Color ObjectColor
		{
			get { return Colors.Black; }
		}
	}
}
