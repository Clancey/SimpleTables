using System;
using MonoTouch.UIKit;

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
			var indexPath = tv.IndexPathForRowAtPoint (point);
			if (indexPath == null)
				return;
			LongPressOnItem (ItemFor(indexPath.Section,indexPath.Row));
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return RowsInSection (section);
		}
		public override int NumberOfSections (UITableView tableView)
		{
			bindLongTouch (tableView);
			return NumberOfSections ();
		}
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var icell = GetICell (indexPath.Section, indexPath.Row);
			if(icell == null)
				return null;
			return icell.GetCell(tableView);
		}
		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var item = ItemFor (indexPath.Section, indexPath.Row);
			if (item is Cell)
				(item as Cell).Selected (tableView, indexPath);
			RowSelected (item);
		}	
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return SectionIndexTitles ();
		}
		
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return HeaderForSection(section);
		}
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{
			var header = GetHeaderICell (section);
			if (header == null)
				return null;
			return header.GetCell(tableView);
		}
	}
}

