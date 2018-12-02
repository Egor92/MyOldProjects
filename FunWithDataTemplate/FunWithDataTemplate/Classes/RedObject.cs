using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class RedObject : BaseObject
	{
		protected override string DefaultText
		{
			get { return "Red"; }
		}

		protected override Color ObjectColor
		{
			get { return Colors.Red; }
		}
	}
}
