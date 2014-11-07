using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T> : UITableViewSource 
	{
		public TableViewModel ()
		{
			
		}
		bool hasBoundLongTouch;
		protected UITableView tv;
		UILongPressGestureRecognizer gesture;
		void bindLongTouch(UITableView tableview)
		{
			if (hasBoundLongTouch)
				return;
			hasBoundLongTouch = true;
			gesture = new UILongPressGestureRecognizer (LongPress);
			tableview.AddGestureRecognizer (gesture);
			tv = tableview;
		}
		public void LongPress(UILongPressGestureRecognizer gesture)
		{
			var point = gesture.LocationInView (tv);
			LongPress (point);
		}

		public void LongPress(PointF point)
		{
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
			tv.DeselectRow (indexPath, true);
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

		public override bool RespondsToSelector (MonoTouch.ObjCRuntime.Selector sel)
		{
			if (sel.Name == "tableView:viewForHeaderInSection:") {
				return GetViewForHeader (tv, 0) != null;
			}
			return base.RespondsToSelector (sel);

		}

		public override UIView GetViewForHeader (UITableView tableView, int section)
		{
			var header = GetHeaderICell (section);
			if (header == null)
				return null;
			return header.GetCell(tableView);
		}

		public void ReloadData()
		{
			if (tv != null)
				tv.ReloadData ();
		}

		public virtual void ClearEvents()
		{
			if (gesture != null) {
				tv.RemoveGestureRecognizer (gesture);
				gesture = null;
				tv = null;
				hasBoundLongTouch = false;
			}

			if(CellFor != null)
			foreach (var d in CellFor.GetInvocationList())
				CellFor -= (GetCellEventHandler)d;

			if(CellForHeader != null)
			foreach (var d in CellForHeader.GetInvocationList())
				CellForHeader -= (GetHeaderCellEventHandler)d;

			if(ItemSelected != null)
			foreach (var d in ItemSelected.GetInvocationList())
				ItemSelected -= (EventHandler<EventArgs<T>>)d;

			if(ItemLongPressed != null)
			foreach (var d in ItemLongPressed.GetInvocationList())
				ItemLongPressed -= (EventHandler<EventArgs<T>>)d;
		}


	}
}

