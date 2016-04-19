using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace SimpleTables
{
	public partial class CollapsableTableViewModal<T> : TableViewModel<T>
	{
		public CollapsableTableViewModal ()
		{
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if(IsCollapsed((int)section))
				return 0;
			return RowsInSection ((int)section);
		}
		void ReloadData(bool collapsed,int section, bool animated)
		{
			List<NSIndexPath> indexPathsToInsert = new List<NSIndexPath>();
			for (int i = 0; i < this.RowsInSection(section); i++) {
				indexPathsToInsert.Add(NSIndexPath.FromRowSection(i,section));
			}

			if(animated)
				BeginAnimation();
			if(!collapsed)
				tv.InsertRows(indexPathsToInsert.ToArray(),UITableViewRowAnimation.Top);
			else
				tv.DeleteRows(indexPathsToInsert.ToArray(),UITableViewRowAnimation.Middle);
			if(animated)
				EndAnimation();
		}

		void BeginAnimation()
		{
			try{
				tv.BeginUpdates();
			}
			catch(Exception ex) {
				Console.WriteLine (ex);
			}
		}
		void EndAnimation()
		{
			try{
				tv.EndUpdates();
			}
			catch(Exception ex) {
				Console.WriteLine (ex);
			}
		}

	}
}

