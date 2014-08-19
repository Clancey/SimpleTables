using System;
using UIKit;
using CoreGraphics;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T> : UITableViewSource 
	{
		public TableViewModel ()
		{
			
		}
		bool hasBoundLongTouch;
		protected UITableView tv;
		void bindLongTouch(UITableView tableview)
		{
			if (hasBoundLongTouch)
				return;
			hasBoundLongTouch = true;
			var gesture = new UILongPressGestureRecognizer (LongPress);
			tableview.AddGestureRecognizer (gesture);
			tv = tableview;
		}
		public void LongPress(UILongPressGestureRecognizer gesture)
		{
			var point = gesture.LocationInView (tv);
			LongPress (point);
		}

		public void LongPress(CGPoint point)
		{
			var indexPath = tv.IndexPathForRowAtPoint (point);
			if (indexPath == null)
				return;
			LongPressOnItem (ItemFor(indexPath.Section,indexPath.Row));
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return (nint)RowsInSection ((int)section);
		}
		public override nint NumberOfSections (UITableView tableView)
		{
			bindLongTouch (tableView);
			return NumberOfSections ();
		}
		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var icell = GetICell (indexPath.Section, indexPath.Row);
			if(icell == null)
				return null;
			return icell.GetCell(tableView);
		}
		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var item = ItemFor (indexPath.Section, indexPath.Row);
			if (item is Cell)
				(item as Cell).Selected (tableView, indexPath);
			tv.DeselectRow (indexPath, true);
			RowSelected (item);
		}	
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return SectionIndexTitles ();
		}
		
		
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return HeaderForSection((int)section);
		}

		public override bool RespondsToSelector (ObjCRuntime.Selector sel)
		{
			if (sel.Name == "tableView:viewForHeaderInSection:") {
				return GetViewForHeader (tv, 0) != null;
			}
			return base.RespondsToSelector (sel);

		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			var header = GetHeaderICell ((int)section);
			if (header == null)
				return null;
			return header.GetCell(tableView);
		}

		public void ReloadData()
		{
			if (tv != null)
				tv.ReloadData ();
		}
	}
}

