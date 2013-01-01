using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;

namespace Xamarin.Tables
{
	public partial class TableViewListModel<T>
	{
		public TableViewListModel (Context context,ListView listView,List<T> items, int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1) : base(context, listView,sectionedListSeparatorLayout)
		{
			Items = items;
		}
	}
}

