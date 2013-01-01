using System;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;

namespace Xamarin.Tables
{
	public partial class TableViewCellModel
	{
		public TableViewCellModel (Context context,ListView listView,List<Cell> items, int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1) : base(context, listView, items,sectionedListSeparatorLayout)
		{
			Items = items;
		}
	}
}

