using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ClassLibrary.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ItemsCollectionAttribute : Attribute
	{
		private List<TabItem> tabItems;

		public ItemsCollectionAttribute(params string[] tabItemsNames)
		{
			tabItems = new List<TabItem>();
			foreach (string tabItemsName in tabItemsNames)
			{
				tabItems.Add(new TabItem() 
				{
					Header = tabItemsName,
					Content = new ScrollViewer() 
					{ 
						VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
						Content = new StackPanel() { Tag = 0 } 
					} 
				});
			}
		}

		public List<TabItem> GetItemsCollection()
		{
			return this.tabItems;
		}
	}
}
