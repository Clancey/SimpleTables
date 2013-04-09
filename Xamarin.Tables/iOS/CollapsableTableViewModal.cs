using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace Xamarin.Tables
{
	public partial class CollapsableTableViewModal<T> : TableViewModel<T>
	{
		public CollapsableTableViewModal ()
		{
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			if(IsCollapsed(section))
				return 0;
			return RowsInSection (section);
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
			
			tv.BeginUpdates();
		}
		void EndAnimation()
		{
			
			tv.EndUpdates();
		}

	}
}

