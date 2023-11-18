using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace SimpleTables
{
	public partial class TableViewSectionModel
	{
		protected virtual void OnSectionAdded (Section section)
		{
			
			if (TableView == null)
				return;

			TableView.InsertSections (MakeIndexSet (Sections.IndexOf (section), 1), UITableViewRowAnimation.Fade);
		}

		protected virtual void OnSectionRemoved (int index)
		{
			if (TableView == null)
				return;

			TableView.DeleteSections (MakeIndexSet (index, 1), UITableViewRowAnimation.None);
		}

		NSIndexSet MakeIndexSet (int start, int count)
		{
			NSRange range;
			range.Location = start;
			range.Length = count;
			return NSIndexSet.FromNSRange (range);
		}
	}
}

