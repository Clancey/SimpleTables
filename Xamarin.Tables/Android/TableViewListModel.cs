using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;

namespace Xamarin.Tables
{
	public partial class TableViewListModel<T>
	{
		public TableViewListModel (List<T> items, int sectionedListSeparatorLayout = global::Android.Resource.Layout.SimpleListItem1) : base(sectionedListSeparatorLayout)
		{
			Items = items;
		}
	}
}

