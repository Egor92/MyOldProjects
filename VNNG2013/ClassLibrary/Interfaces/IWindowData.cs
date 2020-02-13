using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.DBClass;
using System.Windows.Media;

namespace ClassLibrary.Interfaces
{
	public interface IWindowData
	{
		string HeaderText { get; }

		DBObject UpLevel { get; }

		string UpLevelText { get; }

		Brush ForegroundBrush { get; }

		Brush BackgroundBrush { get; }

		Brush BorderBrush { get; }

		DBObject Next { get; }

		DBObject Previous { get; }
	}
}
