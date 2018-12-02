using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class BlueObject : BaseObject
	{
		protected override string DefaultText
		{
			get { return "Blue"; }
		}

		protected override Color ObjectColor
		{
			get { return Colors.Blue; }
		}
	}
}
