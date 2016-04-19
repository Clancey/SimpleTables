using System;
using SimpleTables;
using System.Collections.Generic;
using SimpleTables.Cells;
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
		public SettingsViewModel ()
		{
			Sections = CreateRoot ();
		}

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

