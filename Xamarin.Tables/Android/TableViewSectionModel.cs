using System;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public partial class TableViewSectionModel
	{
		public TableViewSectionModel ( int sectionedListSeparatorLayout = global::Android.Resource.Layout.SimpleListItem1) : base(sectionedListSeparatorLayout)
		{

		}
		
		void ReloadData()
		{
			this.NotifyDataSetChanged ();
		}
	}
}

