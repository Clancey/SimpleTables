using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Xamarin.Tables
{
	public partial class TableViewSectionModel
	{
		UITableView TableView;
		public TableViewSectionModel (UITableView tableview, List<Section> sections) 
		{
			TableView = tableview;
			Sections = sections ?? new List<Section> ();
		}
		public TableViewSectionModel (UITableView tableview)
		{

		}
		void ReloadData()
		{
			if (TableView == null)
				return;
			TableView.ReloadData ();
		}
	}
}

