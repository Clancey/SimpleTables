using System;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;

namespace Xamarin.Tables
{
	public partial class TableViewCellModel
	{
		public TableViewCellModel (List<Cell> items, int sectionedListSeparatorLayout = global::Android.Resource.Layout.SimpleListItem1) : base(items,sectionedListSeparatorLayout)
		{
			Items = items;
		}
	}
}

