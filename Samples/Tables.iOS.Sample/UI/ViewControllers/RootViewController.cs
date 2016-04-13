using System;
using UIKit;
using System.Collections.Generic;
using Xamarin.Tables;

namespace Tables.Sample
{
	public class RootViewController : UITableViewController
	{
		public RootViewController () : base(UITableViewStyle.Grouped)
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.Source = new ItemViewModel();
		}


	}
}

