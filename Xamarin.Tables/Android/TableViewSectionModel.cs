using System;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public partial class TableViewSectionModel
	{
		public TableViewSectionModel (Context context,ListView listView, int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1) : base(context, listView,sectionedListSeparatorLayout)
		{

		}
		
		void ReloadData()
		{
			this.NotifyDataSetChanged ();
		}
	}
}

