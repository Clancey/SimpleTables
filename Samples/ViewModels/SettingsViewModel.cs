using System;
using Xamarin.Tables;
using System.Collections.Generic;
#if iOS
using MonoTouch.UIKit;
#elif Android
using Android.Widget;
using Android.Content;
#endif

namespace Tables.Sample
{
	public class SettingsViewModel : TableViewSectionModel
	{
		#if iOS
		public SettingsViewModel (UITableView tableView) : base(tableView)
		{
			Sections = CreateRoot ();
		}
		#elif Android
		public SettingsViewModel(Context context, ListView listView,int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1) : base(context,listView,sectionedListSeparatorLayout )
		{
			Sections = CreateRoot ();
		}
		
		#endif

		List<Section> CreateRoot()
		{
			return  new List<Section> (){
				new Section ("Settings"){
					new BooleanCell ("Airplane Mode", false),
				},
			};
		}
	}
}

