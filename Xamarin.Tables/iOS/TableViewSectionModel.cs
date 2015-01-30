using System;
using System.Collections.Generic;
using UIKit;

namespace Xamarin.Tables
{
	public partial class TableViewSectionModel
	{
		UITableView tableView;

		public UITableView TableView {
			get {
				return tableView;
			}
			set {
				tableView = value;
			}
		}

		public TableViewSectionModel (UITableView tableview, List<Section> sections) 
		{
			this.tableView = tableview;
			Sections = sections ?? new List<Section> ();
		}
		public TableViewSectionModel (UITableView tableview)
		{
			this.tableView = tableview;
		}
		void ReloadData()
		{
			if (tableView == null)
				return;
			tableView.ReloadData ();
		}
	}
}

