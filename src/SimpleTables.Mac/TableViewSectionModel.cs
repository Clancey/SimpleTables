using System;
using System.Collections.Generic;
using Foundation;

namespace SimpleTables
{
	public partial class TableViewSectionModel
	{
		protected virtual void OnSectionAdded (Section section)
		{
			if (TableView == null)
				return;

			TableView.InsertRows (MakeIndexSet (Sections.IndexOf(section), 1), AppKit.NSTableViewAnimation.SlideUp);
		}

		protected virtual void OnSectionRemoved (int index)
		{
			if (TableView == null)
				return;

			TableView.RemoveRows (MakeIndexSet (index, 1), AppKit.NSTableViewAnimation.SlideDown);
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

