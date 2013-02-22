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
	public class RootViewModel : TableViewSectionModel
	{
#if iOS
		public RootViewModel (UITableView tableView) : base(tableView)
		{
			Sections = CreateRoot ();
		}
#elif Android
		public RootViewModel(Context context, ListView listView,int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1) : base(context,listView,sectionedListSeparatorLayout )
		{
			Sections = CreateRoot ();
		}

#endif
		List<Section> CreateRoot()
		{
			return  new List<Section> (){
				new Section ("Element API"){
					new RadioCell("Test Radio",TestEnum.Value1),
					new StringCell ("iPhone Settings Sample"),
					new StringCell ("Dynamically load data"),
					new StringCell ("Add/Remove demo"),
					new StringCell ("Assorted cells"),
					//new StyledStringCell ("Styled Elements", DemoStyled) { BackgroundUri = new Uri ("file://" + p) },
					new StringCell ("Load More Sample"),
					new StringCell ("Row Editing Support"),
					new StringCell ("Advanced Editing Support"),
					new StringCell ("Owner Drawn Element"),
				},

				new Section ("Container features"){
					new StringCell ("Pull to Refresh"),
					new StringCell ("Headers and Footers"),
					new StringCell ("Root Style"),
					new StringCell ("Index sample"),
				},
				new Section ("Auto-mapped"){
					new StringCell ("Reflection API")
				},
			};
		}
	}
}

