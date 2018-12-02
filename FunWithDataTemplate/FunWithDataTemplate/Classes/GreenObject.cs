using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace FunWithDataTemplate
{
	class GreenObject : BaseObject
	{
		protected override string DefaultText
		{
			get { return "Green"; }
		}

		protected override Color ObjectColor
		{
			get { return Colors.Green; }
		}
	}
}
